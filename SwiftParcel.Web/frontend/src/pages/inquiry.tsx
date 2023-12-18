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
  import { Footer } from "../components/footer";
  import { Header } from "../components/header";
  import { Loader } from "../components/loader";
  import { createInquiry, register } from "../utils/api";
import stringToBoolean from "../components/parsing/stringToBoolean";
import booleanToString from "../components/parsing/booleanToString";

export default function Inquiry() {
    const [loading, setLoading] = React.useState(true);
  
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

    const onSubmit = (e: any) => {
      e.preventDefault();
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

          <form className="flex flex-col gap-10 mb-5" onSubmit={onSubmit}>

            <div className="flex gap-6">

                <div className="flex-grow">
                    <Label value="Package parameters:" />
                </div>

                <div className="flex-grow">
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
          ) : null}
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
  