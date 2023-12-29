import {
    Alert,
    Button,
    Checkbox,
    Label,
    Spinner,
    TextInput,
  } from "flowbite-react";
  import React from "react";
  import { HiInformationCircle, HiCheckCircle } from "react-icons/hi";
  import { Footer } from "../../components/footer";
  import { Header } from "../../components/header";
  import { Loader } from "../../components/loader";
  import { createInquiry, register } from "../../utils/api";
import stringToBoolean from "../../components/parsing/stringToBoolean";
import booleanToString from "../../components/parsing/booleanToString";
import { CourierOffers } from "../couriers/courierOffers";


 // TODO: add this interface 
type FormFields = {
    description: string;
    packageWidth: number;
    packageHeight: number;
    packageDepth: number;
    packageWeight: number;
    sourceAddressStreet: string;
    sourceAddressBuildingNumber: string;
    sourceAddressApartmentNumber: string;
    sourceAddressCity: string;
    sourceAddressZipCode: string;
    sourceAddressCountry: string;
    destinationAddressStreet: string;
    destinationAddressBuildingNumber: string;
    destinationAddressApartmentNumber: string;
    destinationAddressCity: string;
    destinationAddressZipCode: string;
    destinationAddressCountry: string;
    pickupDate: string;
    deliveryDate: string;
    priority: string;
    atWeekend: boolean;
    isCompany: boolean;
    vipPackage: boolean;
};
  

type FormErrors = {
    [K in keyof FormFields]?: string;
};

const TextInputWithLabel = ({ id, label, value, onChange, type = "text",  error }) => (
    <div className="mb-4 flex flex-col">
      <Label htmlFor={id} className="mb-2 block text-sm font-medium text-gray-700">{label}</Label>
      <TextInput 
        id={id} 
        type="text" 
        value={value} 
        onChange={(e) => onChange(e)}
        className={`border-gray-300 focus:ring-blue-500 focus:border-blue-500 block w-full shadow-sm sm:text-sm rounded-md ${error ? 'border-red-500' : ''}`}
      />
      {error && <p className="text-red-500 text-sm mt-1">{error}</p>}
    </div>
);

  
const DateInputWithLabel = ({ id, label, value, onChange }) => {
    // Function to format the date to MongoDB format
    const formatToMongoDBDate = (dateString) => {
        const date = new Date(dateString);
        return date.toISOString();
    };

    // Function to handle date change
    const handleDateChange = (e) => {
        const formattedDate = formatToMongoDBDate(e.target.value);
        console.log("FormattedDate is: ...", formattedDate)
        onChange(formattedDate);
    };

    return (
        <div className="mb-4">
            <Label htmlFor={id}>{label}</Label>
            <TextInput 
                id={id} 
                type="date" 
                value={value ? new Date(value).toISOString().split('T')[0] : ''} 
                onChange={handleDateChange} 
            />
        </div>
    );
};

  
const SectionTitle = ({ title }) => (
    <div className="mb-4 border-b border-gray-300 pb-1">
      <h2 className="text-xl font-semibold text-gray-800">{title}</h2>
    </div>
);

const Alerts = ({ error, success }) => (
    <>
        {error && <Alert color="failure">{error}</Alert>}
        {success && <Alert color="success">{success}</Alert>}
    </>
);


const SubmitButton = ({ inquiryLoading }) => (
    <div className="flex justify-end">
        <Button type="submit" disabled={inquiryLoading}>
            {inquiryLoading ? <Spinner /> : "Create new inquiry"}
        </Button>
    </div>
);

const ShortDescriptionSection = ({ formFields, handleDescriptionChange }) => (
    <div className="my-5">
      <Label htmlFor="description">Short Description:</Label>
      <TextInput id="description" value={formFields.description} onChange={handleDescriptionChange('description')} />
    </div>
);
  

const PackageDetailsSection = ({ formFields, handleNumberChange, errors }) => (
    <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
      <TextInputWithLabel id="package-width" label="Width" value={formFields.packageWidth} onChange={handleNumberChange('packageWidth')} type="number" error={errors.packageWidth}/>
      <TextInputWithLabel id="package-height" label="Height" value={formFields.packageHeight} onChange={handleNumberChange('packageHeight')}  type="number" error={errors.packageHeight}/>
      <TextInputWithLabel id="package-depth" label="Depth" value={formFields.packageDepth} onChange={handleNumberChange('packageDepth')}  type="number" error={errors.packageDepth}/>
      <TextInputWithLabel id="package-weight" label="Weight" value={formFields.packageWeight} onChange={handleNumberChange('packageWeight')}  type="number" error={errors.packageWeight}/>
    </div>
);

const DeliveryDetailsSection = ({ formFields, handleDateChange, handlePriorityChange, handleBooleanChange }) => (
    <div className="grid grid-cols-2 gap-4">
      {/* Other inputs for priority, atWeekend, isCompany, vipPackage */}

      <DateInputWithLabel id="pickup-date" label="Pickup Date" value={formFields.pickupDate} onChange={handleDateChange('pickupDate')} />
      <DateInputWithLabel id="delivery-date" label="Delivery Date" value={formFields.deliveryDate} onChange={handleDateChange('deliveryDate')} />

      <div className="col-span-2 grid grid-cols-4 gap-4">
      <div>
        <Label htmlFor="priority">Priority</Label>
        <select 
          id="priority"
          value={formFields.priority}
          onChange={handlePriorityChange('priority')}
          className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-300 focus:ring focus:ring-indigo-200 focus:ring-opacity-50"
        >
          <option value="low">Low</option>
          <option value="high">High</option>
        </select>
      </div>

      <div>
        <Label htmlFor="at-weekend">Delivery at Weekend</Label>
        <select
          id="at-weekend"
          value={formFields.atWeekend}
          onChange={handleBooleanChange('atWeekend')}
          className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-300 focus:ring focus:ring-indigo-200 focus:ring-opacity-50"
        >
          <option value="false">No</option>
          <option value="true">Yes</option>
        </select>
      </div>

      <div>
        <Label htmlFor="is-company">Created by Company</Label>
        <select
          id="is-company"
          value={formFields.isCompany}
          onChange={handleBooleanChange('isCompany')}
          className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-300 focus:ring focus:ring-indigo-200 focus:ring-opacity-50"
        >
          <option value="false">No</option>
          <option value="true">Yes</option>
        </select>
      </div>

      <div>
        <Label htmlFor="vip-package">VIP Delivery</Label>
        <select
          id="vip-package"
          value={formFields.vipPackage}
          onChange={handleBooleanChange('vipPackage')}
          className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-300 focus:ring focus:ring-indigo-200 focus:ring-opacity-50"
        >
          <option value="false">No</option>
          <option value="true">Yes</option>
        </select>
      </div>
    </div>

    </div>
);

const AddressSection = ({ prefix, formFields, handleStringChange, errors}) => (
    <div className="grid grid-cols-2 gap-4">
        <TextInputWithLabel 
            id={`${prefix}-address-street`} 
            label="Street" 
            value={formFields[`${prefix}AddressStreet`]} 
            onChange={handleStringChange(`${prefix}AddressStreet`)} 
            error={errors?.street}
        />
        <TextInputWithLabel 
            id={`${prefix}-address-building-number`} 
            label="Building Number" 
            value={formFields[`${prefix}AddressBuildingNumber`]} 
            onChange={handleStringChange(`${prefix}AddressBuildingNumber`)} 
            error={errors?.buildingNumber}
        />
        <TextInputWithLabel 
            id={`${prefix}-address-apartment-number`} 
            label="Apartment Number (optional)" 
            value={formFields[`${prefix}AddressApartmentNumber`]} 
            onChange={handleStringChange(`${prefix}AddressApartmentNumber`)} 
            error={errors?.apartmentNumber}
        />
        <TextInputWithLabel 
            id={`${prefix}-address-city`} 
            label="City" 
            value={formFields[`${prefix}AddressCity`]} 
            onChange={handleStringChange(`${prefix}AddressCity`)} 
            error={errors?.city}
        />
        <TextInputWithLabel 
            id={`${prefix}-address-zip-code`} 
            label="Zip Code" 
            value={formFields[`${prefix}AddressZipCode`]} 
            onChange={handleStringChange(`${prefix}AddressZipCode`)} 
            error={errors?.zipCode}
        />
        <TextInputWithLabel 
            id={`${prefix}-address-country`} 
            label="Country" 
            value={formFields[`${prefix}AddressCountry`]} 
            onChange={handleStringChange(`${prefix}AddressCountry`)} 
            error={errors?.country}
        />
    </div>
);



export default function CreateInquiry() {
    const [loading, setLoading] = React.useState(true);

     // TODO: implement the form validators
    const [formIsValid, setFormIsValid] = React.useState(true); 

    // TODO: implement the form fields
    // const [formFields, setFormFields] = React.useState<FormFields>({
    //     description: "",
    //     packageWidth: 0,
    //     packageHeight: 0,
    //     packageDepth: 0,
    //     packageWeight: 0,
    //     sourceAddressStreet: "",
    //     sourceAddressBuildingNumber: "",
    //     sourceAddressApartmentNumber: "",
    //     sourceAddressCity: "",
    //     sourceAddressZipCode: "",
    //     sourceAddressCountry: "",
    //     destinationAddressStreet: "",
    //     destinationAddressBuildingNumber: "",
    //     destinationAddressApartmentNumber: "",
    //     destinationAddressCity: "",
    //     destinationAddressZipCode: "",
    //     destinationAddressCountry: "",
    //     pickupDate: "",
    //     deliveryDate: "",
    //     priority: "low",
    //     atWeekend: false,
    //     isCompany: false,
    //     vipPackage: false
    // });

    const [formFields, setFormFields] = React.useState<FormFields>({
        description: "Test",
        packageWidth: 0.05,
        packageHeight: 0.05,
        packageDepth: 0.05,
        packageWeight: 0.5,
        sourceAddressStreet: "Plac politechniki",
        sourceAddressBuildingNumber: "1",
        sourceAddressApartmentNumber: "",
        sourceAddressCity: "Warszawa",
        sourceAddressZipCode: "00-420",
        sourceAddressCountry: "Polska",
        destinationAddressStreet: "Koszykowa",
        destinationAddressBuildingNumber: "21",
        destinationAddressApartmentNumber: "37",
        destinationAddressCity: "Warszawa",
        destinationAddressZipCode: "00-420",
        destinationAddressCountry: "Polska",
        pickupDate: "2023-12-22", // Format this as per your requirement
        deliveryDate: "2023-12-29", // Format this as per your requirement
        priority: "Low",
        atWeekend: true,
        isCompany: false,
        vipPackage: false
    });
    
    
    const handleNumberChange = <T extends keyof FormFields>(field: T) => (event: React.ChangeEvent<HTMLInputElement>) => {
        const newValue = parseFloat(event.target.value);
        setFormFields(prevState => ({
            ...prevState,
            [field]: isNaN(newValue) ? 0 : newValue
        }));
    };

    const handleStringChange = <T extends keyof FormFields>(field: T) => (event: React.ChangeEvent<HTMLInputElement>) => {
        const newValue = event.target.value;
        setFormFields(prevState => ({
            ...prevState,
            [field]: newValue
        }));
    };

    const handleDateChange = <T extends keyof FormFields>(field: T) => (event: React.ChangeEvent<HTMLInputElement>) => {
        const newValue = event.target.value;
        setFormFields(prevState => ({
            ...prevState,
            [field]: newValue
        }));
    };

    const handlePriorityChange = <T extends keyof FormFields>(field: T) => (event: React.ChangeEvent<HTMLInputElement>) => {
        const newValue = event.target.value;
        setFormFields(prevState => ({
            ...prevState,
            [field]: newValue
        }));
    };

    const handleBooleanChange = <T extends keyof FormFields>(field: T) => (event: React.ChangeEvent<HTMLInputElement>) => {
        const newValue = event.target.value;
        setFormFields(prevState => ({
            ...prevState,
            [field]: newValue
        }));
    };

    const [formErrors, setFormErrors] = React.useState<FormErrors>({});

    const [offers, setOffers] = React.useState(null);
    const [showOffers, setShowOffers] = React.useState(false); // New state to control display

    // const [description, setDescription] = React.useState("");
    // const [packageWidth, setPackageWidth] = React.useState(0);
    // const [packageHeight, setPackageHeight] = React.useState(0);
    // const [packageDepth, setPackageDepth] = React.useState(0);
    // const [packageWeight, setPackageWeight] = React.useState(0);

    // const [sourceAddressStreet, setSourceAddressStreet] = React.useState("");
    // const [sourceAddressBuildingNumber, setSourceAddressBuildingNumber] = React.useState("");
    // const [sourceAddressApartmentNumber, setSourceAddressApartmentNumber] = React.useState("");
    // const [sourceAddressCity, setSourceAddressCity] = React.useState("");
    // const [sourceAddressZipCode, setSourceAddressZipCode] = React.useState("");
    // const [sourceAddressCountry, setSourceAddressCountry] = React.useState("");

    // const [destinationAddressStreet, setDestinationAddressStreet] = React.useState("");
    // const [destinationAddressBuildingNumber, setDestinationAddressBuildingNumber] = React.useState("");
    // const [destinationAddressApartmentNumber, setDestinationAddressApartmentNumber] = React.useState("");
    // const [destinationAddressCity, setDestinationAddressCity] = React.useState("");
    // const [destinationAddressZipCode, setDestinationAddressZipCode] = React.useState("");
    // const [destinationAddressCountry, setDestinationAddressCountry] = React.useState("");

    // const [pickupDate, setPickupDate] = React.useState("");
    // const [deliveryDate, setDeliveryDate] = React.useState("");
    // const [priority, setPriority] = React.useState("low");
    // const [atWeekend, setAtWeekend] = React.useState(false);
    // const [isCompany, setIsCompany] = React.useState(false);
    // const [vipPackage, setVipPackage] = React.useState(false);


    // const [description, setDescription] = React.useState("Test");
    // const [packageWidth, setPackageWidth] = React.useState(0.05);
    // const [packageHeight, setPackageHeight] = React.useState(0.05);
    // const [packageDepth, setPackageDepth] = React.useState(0.05);
    // const [packageWeight, setPackageWeight] = React.useState(0.5);
    // const [sourceAddressStreet, setSourceAddressStreet] = React.useState("Plac politechniki");
    // const [sourceAddressBuildingNumber, setSourceAddressBuildingNumber] = React.useState("1");
    // const [sourceAddressApartmentNumber, setSourceAddressApartmentNumber] = React.useState("");
    // const [sourceAddressCity, setSourceAddressCity] = React.useState("Warszawa");
    // const [sourceAddressZipCode, setSourceAddressZipCode] = React.useState("00-420");
    // const [sourceAddressCountry, setSourceAddressCountry] = React.useState("Polska");
    // const [destinationAddressStreet, setDestinationAddressStreet] = React.useState("Koszykowa");
    // const [destinationAddressBuildingNumber, setDestinationAddressBuildingNumber] = React.useState("21");
    // const [destinationAddressApartmentNumber, setDestinationAddressApartmentNumber] = React.useState("37");
    // const [destinationAddressCity, setDestinationAddressCity] = React.useState("Warszawa");
    // const [destinationAddressZipCode, setDestinationAddressZipCode] = React.useState("00-420");
    // const [destinationAddressCountry, setDestinationAddressCountry] = React.useState("Polska");
    // const [pickupDate, setPickupDate] = React.useState("2023/12/22"); // Format this as per your requirement
    // const [deliveryDate, setDeliveryDate] = React.useState("2023/12/29"); // Format this as per your requirement
    // const [priority, setPriority] = React.useState("Low");
    // const [atWeekend, setAtWeekend] = React.useState(true);
    // const [isCompany, setIsCompany] = React.useState(false);
    // const [vipPackage, setVipPackage] = React.useState(false);

  
    const formatDateForServer = (dateString) => {
        return new Date(dateString).toISOString(); // Adjust this based on server's expected format
      };

      
    const [error, setError] = React.useState("");
    const [success, setSuccess] = React.useState("");
  
    const [inquiryLoading, setInquiryLoading] = React.useState(false);

     // TODO: implement the form validator
    const validateForm = () => {
        const errors: FormErrors = {};

        // if (!formFields.description) {
        //   errors.description = "Description is required";
        // }
        // Add your other validation checks here and update the errors object accordingly
        // For example:
        // if (formFields.packageWidth <= 0) {
        //   errors.packageWidth = "Width must be greater than 0";
        // }
        // Continue for other fields...
    
        setFormErrors(errors);
        return Object.keys(errors).length === 0; 
      };

    const handleSelectOffer = (offer) => {
        // Logic to proceed with the selected offer
        console.log("Selected Offer: ", offer);
        // Additional logic here
    };

    const onSubmit = (e: any) => {
      e.preventDefault();

      const isFormValid = validateForm();
      setFormIsValid(isFormValid);

      if (!isFormValid) {
         return;
      }


      if (inquiryLoading) return;
      setError("");
      setSuccess("");
      setInquiryLoading(true);
  
      createInquiry(formFields.description, formFields.packageWidth, formFields.packageHeight, formFields.packageDepth,
        formFields.packageWeight, formFields.sourceAddressStreet, formFields.sourceAddressBuildingNumber,
        formFields.sourceAddressApartmentNumber, formFields.sourceAddressCity, formFields.sourceAddressZipCode,
        formFields.sourceAddressCountry, formFields.destinationAddressStreet, formFields.destinationAddressBuildingNumber,
        formFields.destinationAddressApartmentNumber, formFields.destinationAddressCity, formFields.destinationAddressZipCode,
        formFields.destinationAddressCountry, formFields.priority, formFields.atWeekend, formatDateForServer(formFields.pickupDate),
        formatDateForServer(formFields.deliveryDate), formFields.isCompany, formFields.vipPackage)
        .then(({ inquiry, offers }) => {
          setSuccess("Inquiry created successfully!");
          setFormFields(
          {
            description: "",
            packageWidth: 0,
            packageHeight: 0,
            packageDepth: 0,
            packageWeight: 0,
            sourceAddressStreet: "",
            sourceAddressBuildingNumber: "",
            sourceAddressApartmentNumber: "",
            sourceAddressCity: "",
            sourceAddressZipCode: "",
            sourceAddressCountry: "",
            destinationAddressStreet: "",
            destinationAddressBuildingNumber: "",
            destinationAddressApartmentNumber: "",
            destinationAddressCity: "",
            destinationAddressZipCode: "",
            destinationAddressCountry: "",
            pickupDate: "",
            deliveryDate: "",
            priority: "Low",
            atWeekend: false,
            isCompany: false,
            vipPackage: false
        });

          console.log("pickupDate: ", formFields.pickupDate)
          setOffers(offers);
          if (offers && offers.length > 0) {
            setShowOffers(true); // Show offers if available
          } else {
            setShowOffers(false); // Hide offers if none are available
          }
          if (offers) {
            // TODO: Implement logic to display the offers
          }
        })

        .catch((err) => {
          setError(err?.response?.data?.message || "Something went wrong!");
        })
        .finally(() => {
          setInquiryLoading(false);
        });
    };
  
    return (

        
      <>
        {loading ? <Loader /> : null}

        {showOffers ? (
          <>
            <h2 className="text-2xl font-bold mb-4">Select a Courier Offer</h2>
            <CourierOffers offers={offers} onSelectOffer={handleSelectOffer} />
          </>
        ) : (
        <div className="container mx-auto px-4">
          <Header loading={loading} setLoading={setLoading} />
          <h1 className="mb-2 text-3xl font-bold text-gray-900 dark:text-white">
            Create an inquiry
          </h1>
          <p className="mb-5">
            Please fill all fields below and click blue button located at the bottom of this page.
          </p>

          <form className="flex flex-col gap-6 px-10" onSubmit={onSubmit}>

            <div className=" gap-6">

                <SectionTitle title="Package Details" />

                <PackageDetailsSection
                    formFields={formFields}
                    handleNumberChange={handleNumberChange}
                    errors={formErrors} />
                
                <SectionTitle title="Source Address" />

                <AddressSection 
                    prefix="source" 
                    formFields={formFields}
                    handleStringChange={handleStringChange}
                    errors={formErrors}
                />

                <SectionTitle title="Destination Address" />
                <AddressSection 
                    prefix="destination" 
                    formFields={formFields}
                    handleStringChange={handleStringChange}
                    errors={formErrors}
                />

                <SectionTitle title="Delivery Details" />
                <DeliveryDetailsSection
                    formFields={formFields}
                    handleDateChange={handleDateChange}
                    handlePriorityChange={handlePriorityChange}
                    handleBooleanChange={handleBooleanChange}
                />

                <ShortDescriptionSection
                    formFields={formFields}
                    handleDescriptionChange={handleStringChange}
                />

                {!formIsValid && <p className="text-red-500">Please fill in all required fields.</p>}

                <Alerts error={error} success={success} />

                <SubmitButton inquiryLoading={inquiryLoading} />
                
                </div>

                {/* <div className="flex-grow">
                    <div className="mb-2 block">
                        <Label htmlFor="package-width" value="Width" />
                    </div>
                    <TextInput
                        id="package-width"
                        type="number"
                        min="0.001"
                        step="0.001"
                        required={true}
                        shadow={true}
                        value={packageWidth}
                        onChange={(e) => setPackageWidth(parseFloat(e.target.value))}
                    />
                </div>
                <div className="flex-grow">
                    <div className="mb-2 block">
                        <Label htmlFor="package-height" value="Height" />
                    </div>
                    <TextInput
                        id="package-height"
                        type="number"
                        min="0.001"
                        step="0.001"
                        required={true}
                        shadow={true}
                        value={packageHeight}
                        onChange={(e) => setPackageHeight(parseFloat(e.target.value))}
                    />
                </div>
                <div className="flex-grow">
                    <div className="mb-2 block">
                        <Label htmlFor="package-depth" value="Depth" />
                    </div>
                    <TextInput
                        id="package-depth"
                        type="number"
                        min="0.001"
                        step="0.001"
                        required={true}
                        shadow={true}
                        value={packageDepth}
                        onChange={(e) => setPackageDepth(parseFloat(e.target.value))}
                    />
                </div>
                <div className="flex-grow">
                    <div className="mb-2 block">
                        <Label htmlFor="package-weight" value="Weight" />
                    </div>
                    <TextInput
                        id="package-weight"
                        type="number"
                        min="0.001"
                        step="0.001"
                        required={true}
                        shadow={true}
                        value={packageWeight}
                        onChange={(e) => setPackageWeight(parseFloat(e.target.value))}
                    />
                </div>
            </div>

            <div className="flex gap-6">

                <div className="flex-grow" style={{ width: '5%' }}>
                    <Label value="Source address:" />
                </div>

                <div className="flex flex-col gap-3 mb-5 flex-grow">

                    <div className="flex gap-6">
                        <div className="flex-grow">
                            <div className="mb-2 block">
                                <Label htmlFor="source-address-street" value="Street" />
                            </div>
                            <TextInput
                                id="source-address-street"
                                type="text"
                                required={true}
                                shadow={true}
                                value={sourceAddressStreet}
                                onChange={(e) => setSourceAddressStreet(e.target.value)}
                            />
                        </div>
                        <div className="flex-grow">
                            <div className="mb-2 block">
                                <Label htmlFor="source-address-building-number" value="Building number" />
                            </div>
                            <TextInput
                                id="source-address-building-number"
                                type="text"
                                required={true}
                                shadow={true}
                                value={sourceAddressBuildingNumber}
                                onChange={(e) => setSourceAddressBuildingNumber(e.target.value)}
                            />
                        </div>
                        <div className="flex-grow">
                            <div className="mb-2 block">
                                <Label htmlFor="source-address-apartment-number" value="Apartment number (optional)" />
                            </div>
                            <TextInput
                                id="source-address-apartment-number"
                                type="text"
                                required={false}
                                shadow={true}
                                value={sourceAddressApartmentNumber}
                                onChange={(e) => setSourceAddressApartmentNumber(e.target.value)}
                            />
                        </div>
                    </div>
                    
                    <div className="flex gap-6">
                        <div className="flex-grow">
                            <div className="mb-2 block">
                                <Label htmlFor="source-address-city" value="City" />
                            </div>
                            <TextInput
                                id="source-address-city"
                                type="text"
                                required={true}
                                shadow={true}
                                value={sourceAddressCity}
                                onChange={(e) => setSourceAddressCity(e.target.value)}
                            />
                        </div>
                        <div className="flex-grow">
                            <div className="mb-2 block">
                                <Label htmlFor="source-address-zip-code" value="Zip Code" />
                            </div>
                            <TextInput
                                id="source-address-zip-code"
                                type="text"
                                required={true}
                                shadow={true}
                                value={sourceAddressZipCode}
                                onChange={(e) => setSourceAddressZipCode(e.target.value)}
                            />
                        </div>
                        <div className="flex-grow">
                            <div className="mb-2 block">
                                <Label htmlFor="source-address-country" value="Country" />
                            </div>
                            <TextInput
                                id="source-address-country"
                                type="text"
                                required={true}
                                shadow={true}
                                value={sourceAddressCountry}
                                onChange={(e) => setSourceAddressCountry(e.target.value)}
                            />
                        </div>
                    </div>
                </div>
            </div>

            <div className="flex gap-6">

                <div className="flex-grow" style={{ width: '5%' }}>
                    <Label value="Destination address:" />
                </div>

                <div className="flex flex-col gap-3 mb-5 flex-grow">
                    <div className="flex gap-6">
                        <div className="flex-grow">
                            <div className="mb-2 block">
                                <Label htmlFor="destination-address-street" value="Street" />
                            </div>
                            <TextInput
                                id="destination-address-street"
                                type="text"
                                required={true}
                                shadow={true}
                                value={destinationAddressStreet}
                                onChange={(e) => setDestinationAddressStreet(e.target.value)}
                            />
                        </div>
                        <div className="flex-grow">
                            <div className="mb-2 block">
                                <Label htmlFor="destination-address-building-number" value="Building number" />
                            </div>
                            <TextInput
                                id="destination-address-building-number"
                                type="text"
                                required={true}
                                shadow={true}
                                value={destinationAddressBuildingNumber}
                                onChange={(e) => setDestinationAddressBuildingNumber(e.target.value)}
                            />
                        </div>
                        <div className="flex-grow">
                            <div className="mb-2 block">
                                <Label htmlFor="destination-address-apartment-number" value="Apartment number (optional)" />
                            </div>
                            <TextInput
                                id="destination-address-apartment-number"
                                type="text"
                                required={false}
                                shadow={true}
                                value={destinationAddressApartmentNumber}
                                onChange={(e) => setDestinationAddressApartmentNumber(e.target.value)}
                            />
                        </div>
                    </div>
                    
                    <div className="flex gap-6">
                        <div className="flex-grow">
                            <div className="mb-2 block">
                                <Label htmlFor="destination-address-city" value="City" />
                            </div>
                            <TextInput
                                id="destination-address-city"
                                type="text"
                                required={true}
                                shadow={true}
                                value={destinationAddressCity}
                                onChange={(e) => setDestinationAddressCity(e.target.value)}
                            />
                        </div>
                        <div className="flex-grow">
                            <div className="mb-2 block">
                                <Label htmlFor="destination-address-zip-code" value="Zip Code" />
                            </div>
                            <TextInput
                                id="destination-address-zip-code"
                                type="text"
                                required={true}
                                shadow={true}
                                value={destinationAddressZipCode}
                                onChange={(e) => setDestinationAddressZipCode(e.target.value)}
                            />
                        </div>
                        <div className="flex-grow">
                            <div className="mb-2 block">
                                <Label htmlFor="destination-address-country" value="Country" />
                            </div>
                            <TextInput
                                id="destination-address-country"
                                type="text"
                                required={true}
                                shadow={true}
                                value={destinationAddressCountry}
                                onChange={(e) => setDestinationAddressCountry(e.target.value)}
                            />
                        </div>
                    </div>
                </div>
            </div>

            <div className="flex gap-6">
                <div className="flex-grow">
                    <div className="mb-2 block">
                        <Label htmlFor="pickup-date" value="Pickup date" />
                    </div>
                    <TextInput
                        id="pickup-date"
                        type="date"
                        required={true}
                        shadow={true}
                        value={pickupDate}
                        onChange={(e) => setPickupDate(e.target.value)}
                    />
                </div>
                <div className="flex-grow">
                    <div className="mb-2 block">
                        <Label htmlFor="delivery-date" value="Delivery date" />
                    </div>
                    <TextInput
                        id="delivery-date"
                        type="date"
                        required={true}
                        shadow={true}
                        value={deliveryDate}
                        onChange={(e) => setDeliveryDate(e.target.value)}
                    />
                </div>
                <div className="flex-grow" style={{ width: '5%' }}>
                    <div className="mb-2 block">
                        <Label htmlFor="priority" value="Priority" />
                    </div>
                    <select style={{ width: '100%' }}
                        id="priority"
                        onChange={(e) => setPriority(e.target.value)}>
                        <option value="low" selected>Low</option>
                        <option value="high">High</option>
                    </select>
                </div>
                <div className="flex-grow" style={{ width: '5%' }}>
                    <div className="mb-2 block">
                        <Label htmlFor="at-weekend" value="Delivery at weekend" />
                    </div>
                    <select style={{ width: '100%' }}
                        id="at-weekend"
                        onChange={(e) => setAtWeekend(stringToBoolean(e.target.value))}>
                        <option value="false" selected>No</option>
                        <option value="true">Yes</option>
                    </select>
                </div>
                <div className="flex-grow" style={{ width: '5%' }}>
                    <div className="mb-2 block">
                        <Label htmlFor="is-company" value="Created by company" />
                    </div>
                    <select style={{ width: '100%' }}
                        id="is-company"
                        onChange={(e) => setIsCompany(stringToBoolean(e.target.value))}>
                        <option value="false" selected>No</option>
                        <option value="true">Yes</option>
                    </select>
                </div>
                <div className="flex-grow" style={{ width: '5%' }}>
                    <div className="mb-2 block">
                        <Label htmlFor="vip-package" value="VIP delivery" />
                    </div>
                    <select style={{ width: '100%' }}
                        id="vip-package"
                        onChange={(e) => setVipPackage(stringToBoolean(e.target.value))}>
                        <option value="false" selected>No</option>
                        <option value="true">Yes</option>
                    </select>
                </div>
            </div>

            <div className="flex gap-6">
                <div className="mb-2 block">
                    <Label htmlFor="description" value="Short description:" />
                </div>
                <TextInput className="flex-grow"
                    id="description"
                    type="text"
                    required={true}
                    shadow={true}
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                />
            </div>

            {inquiryLoading ? (
              <Button type="submit" disabled={true}>
                <div className="mr-3">
                  <Spinner size="sm" light={true} />
                </div>
                Creating ...
              </Button>
            ) : (
              <Button type="submit">Create new inquiry</Button>
            )}
          </form>

          {error ? (
            <Alert color="failure" icon={HiInformationCircle} className="mb-3">
              <span>
                <span className="font-bold">Error!</span> {error}
              </span>
            </Alert> 
          ) : null}*/}
          </form>
          {success ? (
            <Alert color="success" icon={HiCheckCircle} className="mb-3">
              <span>
                <span className="font-bold">Success!</span> {success}
              </span>

              <CourierOffers offers={offers} onSelectOffer={handleSelectOffer} />
            </Alert>
          ) : null}
          <Footer />
        </div>
        )}
      </>
    
    );
  }
  