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


type FormFields = {
    description: string;
    packageWidth: number;
    packageHeight: number;
    packageDepth: number;
    packageWeight: number;
    sourceAddressStreet: string;
    destinationAddress: string;
    // Add other fields
};
  

type FormErrors = {
    [K in keyof FormFields]?: string;
};

const TextInputWithLabel = ({ id, label, value, onChange, error }) => (
    <div className="mb-4 flex flex-col">
      <Label htmlFor={id} className="mb-2 block text-sm font-medium text-gray-700">{label}</Label>
      <TextInput 
        id={id} 
        type="text" 
        value={value} 
        onChange={(e) => onChange(e.target.value)} 
        className={`border-gray-300 focus:ring-blue-500 focus:border-blue-500 block w-full shadow-sm sm:text-sm rounded-md ${error ? 'border-red-500' : ''}`}
      />
      {error && <p className="text-red-500 text-sm mt-1">{error}</p>}
    </div>
);

  
const DateInputWithLabel = ({ id, label, value, onChange }) => (
    <div className="mb-4">
        <Label htmlFor={id}>{label}</Label>
        <TextInput id={id} type="date" value={value} onChange={(e) => onChange(e.target.value)} />
    </div>
);

  
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

const ShortDescriptionSection = ({ description, setDescription }) => (
    <div className="my-5">
      <Label htmlFor="description">Short Description:</Label>
      <TextInput id="description" value={description} onChange={(e) => setDescription(e.target.value)} />
    </div>
);
  

const PackageDetailsSection = ({ packageWidth, setPackageWidth, packageHeight, setPackageHeight, packageDepth, setPackageDepth, packageWeight, setPackageWeight, errors }) => (
    <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
      <TextInputWithLabel id="package-width" label="Width" value={packageWidth} onChange={setPackageWidth} error={errors.packageWidth}/>
      <TextInputWithLabel id="package-height" label="Height" value={packageHeight} onChange={setPackageHeight} error={errors.packageHeight}/>
      <TextInputWithLabel id="package-depth" label="Depth" value={packageDepth} onChange={setPackageDepth} error={errors.packageDepth}/>
      <TextInputWithLabel id="package-weight" label="Weight" value={packageWeight} onChange={setPackageWeight} error={errors.packageWeight}/>
    </div>
);

const DeliveryDetailsSection = ({ pickupDate, setPickupDate, deliveryDate, setDeliveryDate, priority, setPriority, atWeekend, setAtWeekend, isCompany, setIsCompany, vipPackage, setVipPackage }) => (
    <div className="grid grid-cols-2 gap-4">
      <DateInputWithLabel id="pickup-date" label="Pickup Date" value={pickupDate} onChange={setPickupDate} />
      <DateInputWithLabel id="delivery-date" label="Delivery Date" value={deliveryDate} onChange={setDeliveryDate} />
      {/* Other inputs for priority, atWeekend, isCompany, vipPackage */}
    </div>
);
  
const AddressSection = ({
    prefix,
    street, setStreet,
    buildingNumber, setBuildingNumber,
    apartmentNumber, setApartmentNumber,
    city, setCity,
    zipCode, setZipCode,
    country, setCountry,
    errors
}) => (
    <div className="grid grid-cols-2 gap-4">
        <TextInputWithLabel id={`${prefix}-address-street`} label="Street" value={street} onChange={e => setStreet(e.target.value)} error={errors?.sourceAddressStreet}/>
        <TextInputWithLabel id={`${prefix}-address-building-number`} label="Building Number" value={buildingNumber} onChange={e => setBuildingNumber(e.target.value)} error={errors?.buildingNumber}/>
        <TextInputWithLabel id={`${prefix}-address-apartment-number`} label="Apartment Number (optional)" value={apartmentNumber} onChange={e => setApartmentNumber(e.target.value)} error={errors.appartmentNumber}/>
        <TextInputWithLabel id={`${prefix}-address-city`} label="City" value={city} onChange={e => setCity(e.target.value)} error={errors.city}/>
        <TextInputWithLabel id={`${prefix}-address-zip-code`} label="Zip Code" value={zipCode} onChange={e => setZipCode(e.target.value)} error={errors.city}/>
        <TextInputWithLabel id={`${prefix}-address-country`} label="Country" value={country} onChange={e => setCountry(e.target.value)} error={errors.country}/>
    </div>
);


  
  
  

  

export default function CreateInquiry() {
    const [loading, setLoading] = React.useState(true);

    const [formIsValid, setFormIsValid] = React.useState(true); 
    const [formFields, setFormFields] = React.useState<FormFields>({
        description: "",
        packageWidth: 0,
        packageHeight: 0,
        packageDepth: 0,
        packageWeight: 0,
        sourceAddressStreet: "",
        destinationAddress: ""
        // Initialize other fields
    });
    
    const [formErrors, setFormErrors] = React.useState<FormErrors>({});

    const [description, setDescription] = React.useState("");
    const [packageWidth, setPackageWidth] = React.useState(0);
    const [packageHeight, setPackageHeight] = React.useState(0);
    const [packageDepth, setPackageDepth] = React.useState(0);
    const [packageWeight, setPackageWeight] = React.useState(0);

    const [sourceAddressStreet, setSourceAddressStreet] = React.useState("");
    const [sourceAddressBuildingNumber, setSourceAddressBuildingNumber] = React.useState("");
    const [sourceAddressApartmentNumber, setSourceAddressApartmentNumber] = React.useState("");
    const [sourceAddressCity, setSourceAddressCity] = React.useState("");
    const [sourceAddressZipCode, setSourceAddressZipCode] = React.useState("");
    const [sourceAddressCountry, setSourceAddressCountry] = React.useState("");

    const [destinationAddressStreet, setDestinationAddressStreet] = React.useState("");
    const [destinationAddressBuildingNumber, setDestinationAddressBuildingNumber] = React.useState("");
    const [destinationAddressApartmentNumber, setDestinationAddressApartmentNumber] = React.useState("");
    const [destinationAddressCity, setDestinationAddressCity] = React.useState("");
    const [destinationAddressZipCode, setDestinationAddressZipCode] = React.useState("");
    const [destinationAddressCountry, setDestinationAddressCountry] = React.useState("");

    const [pickupDate, setPickupDate] = React.useState("");
    const [deliveryDate, setDeliveryDate] = React.useState("");
    const [priority, setPriority] = React.useState("low");
    const [atWeekend, setAtWeekend] = React.useState(false);
    const [isCompany, setIsCompany] = React.useState(false);
    const [vipPackage, setVipPackage] = React.useState(false);
  
    const [error, setError] = React.useState("");
    const [success, setSuccess] = React.useState("");
  
    const [inquiryLoading, setInquiryLoading] = React.useState(false);

    const validateForm = () => {
        const errors: FormErrors = {};

        if (!formFields.description) {
          errors.description = "Description is required";
        }
        // Add your other validation checks here and update the errors object accordingly
        // For example:
        if (formFields.packageWidth <= 0) {
          errors.packageWidth = "Width must be greater than 0";
        }
        // Continue for other fields...
    
        setFormErrors(errors);
        return Object.keys(errors).length === 0; 
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
  
      createInquiry(description, packageWidth, packageHeight, packageDepth, packageWeight,
        sourceAddressStreet, sourceAddressBuildingNumber, sourceAddressApartmentNumber,
        sourceAddressCity, sourceAddressZipCode, sourceAddressCountry,
        destinationAddressStreet, destinationAddressBuildingNumber, destinationAddressApartmentNumber,
        destinationAddressCity, destinationAddressZipCode, destinationAddressCountry, priority, atWeekend,
        `${pickupDate}T00:00:00.000Z`, `${deliveryDate}T00:00:00.000Z`, isCompany, vipPackage)
        .then((res) => {
          setSuccess(
            res?.data?.message || "Inquiry created successfully!"
          );
          setDescription("");
          setPackageWidth(0);
          setPackageHeight(0);
          setPackageDepth(0);
          setPackageWeight(0);
          setSourceAddressCity("");
          setSourceAddressBuildingNumber("");
          setSourceAddressApartmentNumber("");
          setSourceAddressCity("");
          setSourceAddressZipCode("");
          setSourceAddressCountry("");
          setDestinationAddressCity("");
          setDestinationAddressBuildingNumber("");
          setDestinationAddressApartmentNumber("");
          setDestinationAddressCity("");
          setDestinationAddressZipCode("");
          setDestinationAddressCountry("");
          setPickupDate("");
          setDeliveryDate("");
          setPriority("low");
          setAtWeekend(false);
          setIsCompany(false);
          setVipPackage(false);
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

                <PackageDetailsSection {...{ packageWidth, setPackageWidth, packageHeight, setPackageHeight, packageDepth, setPackageDepth, packageWeight, setPackageWeight, errors: formErrors }} />


                <SectionTitle title="Source Address" />
                <AddressSection 
                    prefix="source" 
                    street={sourceAddressStreet} 
                    setStreet={setSourceAddressStreet}
                    buildingNumber={sourceAddressBuildingNumber} 
                    setBuildingNumber={setSourceAddressBuildingNumber}
                    apartmentNumber={sourceAddressApartmentNumber} 
                    setApartmentNumber={setSourceAddressApartmentNumber}
                    city={sourceAddressCity} 
                    setCity={setSourceAddressCity}
                    zipCode={sourceAddressZipCode} 
                    setZipCode={setSourceAddressZipCode}
                    country={sourceAddressCountry} 
                    setCountry={setSourceAddressCountry}
                    errors={formErrors}
                />


                <SectionTitle title="Destination Address" />
                <AddressSection 
                    prefix="destination" 
                    street={destinationAddressStreet} 
                    setStreet={setDestinationAddressStreet}
                    buildingNumber={destinationAddressBuildingNumber} 
                    setBuildingNumber={setDestinationAddressBuildingNumber}
                    apartmentNumber={destinationAddressApartmentNumber} 
                    setApartmentNumber={setDestinationAddressApartmentNumber}
                    city={destinationAddressCity} 
                    setCity={setDestinationAddressCity}
                    zipCode={destinationAddressZipCode} 
                    setZipCode={setDestinationAddressZipCode}
                    country={destinationAddressCountry} 
                    setCountry={setDestinationAddressCountry}
                    errors={formErrors}
                />

                <SectionTitle title="Delivery Details" />
                <DeliveryDetailsSection {...{ pickupDate, setPickupDate, deliveryDate, setDeliveryDate, priority, setPriority, atWeekend, setAtWeekend, isCompany, setIsCompany, vipPackage, setVipPackage }} />

                <ShortDescriptionSection description={description} setDescription={setDescription} />

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
            </Alert>
          ) : null}
          <Footer />
        </div>
      </>
    );
  }
  