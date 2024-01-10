import {
    Alert,
    Button,
    Label,
    Modal,
    TextInput,
    Spinner
  } from "flowbite-react";
  import React from "react";
  import { HiCheckCircle } from "react-icons/hi";
  import { useNavigate } from "react-router-dom";
  import { createOrder } from "../../../utils/api";
  
  interface UserDetailsModalProps {
    show: boolean;
    setShow: (show: boolean) => void;
    userId: any;
    parcelId : any;
  }

  type UserInfo = {
    name: string;
    email: string;
    addressStreet: string;
    addressBuildingNumber: string;
    addressApartmentNumber: string;
    addressCity: string;
    addressZipCode: string;
    addressCountry: string;
  };

  type UserInfoErrors = {
    [K in keyof UserInfo]?: string;
  };

  const SectionTitle = ({ title }) => (
    <div className="mb-4 border-b border-gray-400 pb-1">
      <h2 className="text-xl font-semibold text-gray-800">{title}</h2>
    </div>
  );

  const TextInputWithLabel = ({ id, label, value, onChange, type,  error }) => (
    <div className="mb-4 flex flex-col">
      <Label htmlFor={id} className="mb-2 block text-sm font-medium text-gray-700">{label}</Label>
      <TextInput 
        id={id} 
        type={type}
        lang="en"
        value={value} 
        onChange={(e) => onChange(e)}
        className={`border-gray-300 focus:ring-blue-500 focus:border-blue-500 block w-full shadow-sm sm:text-sm rounded-md ${error ? 'border-red-500' : ''}`}
      />
      {error && <p className="text-red-500 text-sm mt-1">{error}</p>}
    </div>
  );

  const Alerts = ({ error, success }) => (
    <>
        {error && <Alert color="failure">{error}</Alert>}
        {success && <Alert color="success">{success}</Alert>}
    </>
  );


  const SubmitButton = ({ userInfoLoading }) => (
      <div className="flex justify-end">
          <Button type="submit" disabled={userInfoLoading}>
              {userInfoLoading ? <Spinner /> : "Submit the offer"}
          </Button>
      </div>
  );

  const BasicInfoSection = ({ userData, handleStringChange, errors }) => (
    <div>
        <TextInputWithLabel
            id="name"
            label="Your name:"
            value={userData.name}
            onChange={handleStringChange('name')}
            type="text" 
            error={errors.description}
        />
        <TextInputWithLabel
            id="name"
            label="Your email:"
            value={userData.email}
            onChange={handleStringChange('email')}
            type="email" 
            error={errors.description}
        />
    </div>
  );

  const AddressSection = ({ userData, handleStringChange, errors }) => (
    <div className="grid grid-cols-2 gap-4">
        <TextInputWithLabel 
            id={`address-street`} 
            label="Street:" 
            value={userData.addressStreet} 
            onChange={handleStringChange('addressStreet')} 
            type="text" 
            error={errors.addressStreet}
        />
        <TextInputWithLabel 
            id={`address-building-number`} 
            label="Building number:" 
            value={userData.addressBuildingNumber} 
            onChange={handleStringChange('addressBuildingNumber')} 
            type="text" 
            error={errors.addressBuildingNumber}
        />
        <TextInputWithLabel 
            id={`address-apartment-number`} 
            label="Apartment number:" 
            value={userData.addressApartmentNumber} 
            onChange={handleStringChange('addressApartmentNumber')} 
            type="text" 
            error={errors.addressApartmentNumber}
        />
        <TextInputWithLabel 
            id={`address-city`} 
            label="City:" 
            value={userData.addressCity} 
            onChange={handleStringChange('addressCity')} 
            type="text" 
            error={errors.addressCity}
        />
        <TextInputWithLabel 
            id={`address-zip-code`} 
            label="Zip code:" 
            value={userData.addressZipCode} 
            onChange={handleStringChange('addressZipCode')} 
            type="text" 
            error={errors.addressZipCode}
        />
        <TextInputWithLabel 
            id={`address-country`} 
            label="Country:" 
            value={userData.addressCountry} 
            onChange={handleStringChange('addressCountry')} 
            type="text" 
            error={errors.addressCountry}
        />
    </div>
  );

  export function UserDetailsModal(props: UserDetailsModalProps) {
    const close = () => {
      props.setShow(false);
    };

    const submit = async (e: any) => {
      e.preventDefault();
      close();
    };
    
    const [userInfo, setUserInfo] = React.useState<UserInfo>({
      name: "Default name",
      email: "default@email.com",
      addressStreet: "Plac Politechniki",
      addressBuildingNumber: "1",
      addressApartmentNumber: "51",
      addressCity: "Warszawa",
      addressZipCode: "00-007",
      addressCountry: "Polska",
    });

    const handleStringChange = <T extends keyof UserInfo>(field: T) => (event: React.ChangeEvent<HTMLInputElement>) => {
      const newValue = event.target.value;
      setUserInfo(prevState => ({
          ...prevState,
          [field]: newValue
      }));
    };

    // const accept = (orderId: string) => {
    //   const payload = {
    //     OrderId: orderId
    //   };
    //   approvePendingOffer(orderId, JSON.parse(JSON.stringify(payload)));
    //   close();
    // };

    // const reject = (orderId: string, reason: string) => {
    //   const payload = {
    //     OrderId: orderId,
    //     Reason: reason
    //   };
    //   cancelPendingOffer(orderId, JSON.parse(JSON.stringify(payload)));
    //   close();
    // };

    const [formIsValid, setFormIsValid] = React.useState(true); 
    const [userInfoErrors, setUserInfoErrors] = React.useState<UserInfoErrors>({});

    const [error, setError] = React.useState("");
    const [success, setSuccess] = React.useState("");
  
    const [userInfoLoading, setUserInfoLoading] = React.useState(false);

    const navigate = useNavigate();

    const validateForm = () => {
        const errors: UserInfoErrors = {};

        // validation of basic info section

        if (!userInfo.name) {
            errors.name = "Your name is required!";
        }

        if (!userInfo.email) {
            errors.email = "Your email is required!";
        }

        // validation of address section

        if (!userInfo.addressStreet) {
            errors.addressStreet = "Street in your address is required!";
        }

        if (!userInfo.addressBuildingNumber) {
            errors.addressBuildingNumber = "Building number in your address is required!";
        }

        if (!userInfo.addressCity) {
            errors.addressCity = "City in your address is required!";
        }

        if (!userInfo.addressZipCode) {
            errors.addressZipCode = "Zip code in your address is required!";
        }

        if (!userInfo.addressCountry) {
            errors.addressCountry = "Country in your address is required!";
        }

        setUserInfoErrors(errors);
        return Object.keys(errors).length === 0; 
    };

    const onSubmit = (e: any) => {
      e.preventDefault();

      const isFormValid = validateForm();
      setFormIsValid(isFormValid);

      if (!isFormValid) {
          return;
      }

      if (userInfoLoading) return;
      setError("");
      setSuccess("");
      setUserInfoLoading(true);

      createOrder(
        props.userId,
        props.parcelId,
        userInfo.name,
        userInfo.email,
        userInfo.addressStreet,
        userInfo.addressBuildingNumber,
        userInfo.addressApartmentNumber,
        userInfo.addressCity,
        userInfo.addressZipCode,
        userInfo.addressCountry)
        .then((response) => {
          setSuccess("Offer chosen created successfully!");
          setUserInfo(
          {
            name: "",
            email: "",
            addressStreet: "",
            addressBuildingNumber: "",
            addressApartmentNumber: "",
            addressCity: "",
            addressZipCode: "",
            addressCountry: ""
        });

        // navigate("/offers", {state:{parcelId: response}});

        //   setOffers(offers);
        //   if (offers && offers.length > 0) {
        //     setShowOffers(true); // Show offers if available
        //   } else {
        //     setShowOffers(false); // Hide offers if none are available
        //   }
        //   if (offers) {
        //     // TODO: Implement logic to display the offers
        //   }
        })

        .catch((err) => {
          setError(err?.response?.data?.message || "Something went wrong!");
        })
        .finally(() => {
          setUserInfoLoading(false);
        });
    };


    return (
      <React.Fragment>
        <Modal show={props.show} size="4xl" popup={true} onClose={close}>
          <Modal.Header />
          <Modal.Body style={{ overflowY: 'scroll' }}>
            <form onSubmit={submit}>
              <div className="space-y-6 px-6 pb-4 sm:pb-6 lg:px-8 xl:pb-8">
                <div className="space-y-6 gap-6" style={{ maxHeight: '70vh', paddingBottom: '20px' }}>
                  <h1 className="mb-2 text-2xl font-bold text-gray-900 dark:text-white">
                    Fill data if needed:
                  </h1>
                  <form className="flex flex-col gap-6" onSubmit={onSubmit}>
                    <div className=" gap-6">

                        <SectionTitle title="Your basic info" />

                        <BasicInfoSection 
                            userData={userInfo}
                            handleStringChange={handleStringChange}
                            errors={userInfoErrors}
                        />
                        
                        <SectionTitle title="Your address" />

                        <AddressSection 
                            userData={userInfo}
                            handleStringChange={handleStringChange}
                            errors={userInfoErrors}
                        />

                        {!formIsValid && <p className="text-red-500">Please fill in all required fields.</p>}

                        <Alerts error={error} success={success} />

                        <div className="flex flex-col gap-6">
                          <SubmitButton userInfoLoading={userInfoLoading} />
                        </div>
                        
                      </div>
                  </form>
                  {success ? (
                    <div>
                    <Alert color="success" icon={HiCheckCircle} className="mb-3">
                      <span>
                        <span className="font-bold">Success!</span> {success}
                      </span>
                    </Alert>
                    </div>
                  ) : null}
                  <div className="space-y-6 gap-6">

                    <div style={{ marginBottom: '40px' }}></div>

                  </div>
                </div>
              </div>
            </form>
          </Modal.Body>
        </Modal>
      </React.Fragment>
    );
  }
  