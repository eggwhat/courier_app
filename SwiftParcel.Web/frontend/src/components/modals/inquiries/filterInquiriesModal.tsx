import {
    Alert,
    Button,
    Label,
    Modal,
    Spinner,
    TextInput,
  } from "flowbite-react";
  import React from "react";
  import { HiInformationCircle } from "react-icons/hi";
  
  interface FilterInquiriesModalProps {
    show: boolean;
    setShow: (show: boolean) => void;
    inputData: any;
    tableData: any;
    setTableData: any;
  }
  
  type FilteringDetails = {
    patternId: string;
    minWidth: number;
    maxWidth: number;
    minHeight: number;
    maxHeight: number;
    minDepth: number;
    maxDepth: number;
    minWeight: number;
    maxWeight: number;
    patternSourceStreet: string;
    patternSourceBuildingNumber: string;
    patternSourceApartmentNumber: string;
    patternSourceCity: string;
    patternSourceZipCode: string;
    patternSourceCountry: string;
    patternDestinationStreet: string;
    patternDestinationBuildingNumber: string;
    patternDestinationApartmentNumber: string;
    patternDestinationCity: string;
    patternDestinationZipCode: string;
    patternDestinationCountry: string;
    minDateOfInquiring: string;
    maxDateOfInquiring: string;
  };

  const TextInputWithLabel = ({ id, label, value, onChange, type}) => (
    <div className="mb-4 flex flex-col">
      <Label htmlFor={id} className="mb-2 block text-sm font-medium text-gray-700">{label}</Label>
      <TextInput 
        id={id} 
        type={type}
        lang="en"
        value={value} 
        onChange={(e) => onChange(e)}
        className={`border-gray-300 focus:ring-blue-500 focus:border-blue-500 block w-full shadow-sm sm:text-sm rounded-md`}
      />
    </div>
  );

  const DateInputWithLabel = ({ id, label, value, onChange }) => {
    // Function to format the date to MongoDB format
    const formatToMongoDBDate = (dateString : string) => {
        const date = new Date(dateString);
        return date.toISOString();
    };

    // Function to handle date change
    const handleDateChange = (e : any) => {
        const formattedDate = formatToMongoDBDate(e.target.value);
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

  const IdFilterSection = ({ filterData, handleStringChange }) => (
    <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
        <TextInputWithLabel
            id="id-pattern"
            label="Pattern contained in inquiry's id:"
            type="text"
            value={filterData.patternId}
            onChange={handleStringChange('patternId')}
        />
    </div>
  );

  const DimensionsFilterSection = ({ filterData, handleNumberChange }) => (
    <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
        <TextInputWithLabel
            id="width-min"
            label="Minimum width:"
            type="number"
            value={filterData.minWidth}
            onChange={handleNumberChange('minWidth')}
        />
        <TextInputWithLabel
            id="width-max"
            label="Maximum width:"
            type="number"
            value={filterData.maxWidth}
            onChange={handleNumberChange('maxWidth')}
        />

        <TextInputWithLabel
            id="height-min"
            label="Minimum height:"
            type="number"
            value={filterData.minHeight}
            onChange={handleNumberChange('minHeight')}
        />
        <TextInputWithLabel
            id="height-max"
            label="Maximum height:"
            type="number"
            value={filterData.maxHeight}
            onChange={handleNumberChange('maxHeight')}
        />

        <TextInputWithLabel
            id="depth-min"
            label="Minimum depth:"
            type="number"
            value={filterData.minDepth}
            onChange={handleNumberChange('minDepth')}
        />
        <TextInputWithLabel
            id="depth-max"
            label="Maximum depth:"
            type="number"
            value={filterData.maxDepth}
            onChange={handleNumberChange('maxDepth')}
        />
    </div>
  );

  const WeightFilterSection = ({ filterData, handleNumberChange }) => (
    <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
        <TextInputWithLabel
            id="weight-min"
            label="Minimum weight:"
            type="number"
            value={filterData.minWeight}
            onChange={handleNumberChange('minWeight')}
        />
        <TextInputWithLabel
            id="weight-max"
            label="Maximum weight:"
            type="number"
            value={filterData.maxWeight}
            onChange={handleNumberChange('maxWeight')}
        />
    </div>
  );

  const AddressFilterSection = ({ prefix, filterData, handleStringChange }) => (
    <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
        <TextInputWithLabel
            id={`${prefix}-street-pattern`}
            label={`Pattern contained in street:`}
            type="text"
            value={filterData[`pattern${prefix}Street`]}
            onChange={handleStringChange(`pattern${prefix}Street`)} 
        />
        <TextInputWithLabel
            id={`${prefix}-building-number-pattern`}
            label={`Pattern contained in building number:`}
            type="text"
            value={filterData[`pattern${prefix}BuildingNumber`]}
            onChange={handleStringChange(`pattern${prefix}BuildingNumber`)} 
        />

        <TextInputWithLabel
            id={`${prefix}-apartment-number-pattern`}
            label={`Pattern contained in apartment number:`}
            type="text"
            value={filterData[`pattern${prefix}ApartmentNumber`]}
            onChange={handleStringChange(`pattern${prefix}ApartmentNumber`)} 
        />
        <TextInputWithLabel
            id={`${prefix}-city-pattern`}
            label={`Pattern contained in city:`}
            type="text"
            value={filterData[`pattern${prefix}City`]}
            onChange={handleStringChange(`pattern${prefix}City`)} 
        />

        <TextInputWithLabel
            id={`${prefix}-zip-code-pattern`}
            label={`Pattern contained in zip code:`}
            type="text"
            value={filterData[`pattern${prefix}ZipCode`]}
            onChange={handleStringChange(`pattern${prefix}ZipCode`)} 
        />
        <TextInputWithLabel
            id={`${prefix}-city-pattern`}
            label={`Pattern contained in country:`}
            type="text"
            value={filterData[`pattern${prefix}Country`]}
            onChange={handleStringChange(`pattern${prefix}Country`)} 
        />
    </div>
  );

  const DateOfInquiringFilterSection = ({ filterData, handleDateChange }) => (
    <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
        <DateInputWithLabel
            id="date-of-inquiring-min"
            label="From:"
            value={filterData.minDateOfInquiring}
            onChange={handleDateChange('minDateOfInquiring')}
        />
        <DateInputWithLabel
            id="date-of-inquiring-max"
            label="To:"
            value={filterData.maxDateOfInquiring}
            onChange={handleDateChange('maxDateOfInquiring')}
        />
    </div>
  );

  export function FilterInquiriesModal(props: FilterInquiriesModalProps) {
    const close = () => {
      setError("");
      setIsLoading(false);
      props.setShow(false);
    };

    const [filteringDetails, setFilteringDetails] = React.useState<FilteringDetails>({
        patternId: null,
        minWidth: null,
        maxWidth: null,
        minHeight: null,
        maxHeight: null,
        minDepth: null,
        maxDepth: null,
        minWeight: null,
        maxWeight: null,
        patternSourceStreet: null,
        patternSourceBuildingNumber: null,
        patternSourceApartmentNumber: null,
        patternSourceCity: null,
        patternSourceZipCode: null,
        patternSourceCountry: null,
        patternDestinationStreet: null,
        patternDestinationBuildingNumber: null,
        patternDestinationApartmentNumber: null,
        patternDestinationCity: null,
        patternDestinationZipCode: null,
        patternDestinationCountry: null,
        minDateOfInquiring: null,
        maxDateOfInquiring: null
    });

    const handleNumberChange = <T extends keyof FilteringDetails>(field: T) => (event: React.ChangeEvent<HTMLInputElement>) => {
        const newValue = parseFloat(event.target.value);
        setFilteringDetails(prevState => ({
            ...prevState,
            [field]: isNaN(newValue) ? null : newValue
        }));
    };

    const handleStringChange = <T extends keyof FilteringDetails>(field: T) => (event: React.ChangeEvent<HTMLInputElement>) => {
        const newValue = event.target.value;
        setFilteringDetails(prevState => ({
            ...prevState,
            [field]: newValue.length == 0 ? null : newValue
        }));
    };

    const handleDateChange = <T extends keyof FilteringDetails>(field: T) => (event: React.ChangeEvent<HTMLInputElement>) => {
        const newValue = event;
        setFilteringDetails(prevState => ({
            ...prevState,
            [field]: newValue
        }));
    };

    const [isLoading, setIsLoading] = React.useState(false);
    const [error, setError] = React.useState("");

    const submit = async (e: any) => {
      e.preventDefault();
      setError("");
      setIsLoading(true);

      filterInquiries();

      close();
      setIsLoading(false);
    };
  
    const filterInquiries = () => {
      const filteredElements = props.inputData.filter((element : any) =>
        // filtering of id section
        (filteringDetails.patternId == null || element.id.includes(filteringDetails.patternId)) &&

        // filtering of dimensions section
        (filteringDetails.minWidth == null || element.width >= filteringDetails.minWidth) &&
        (filteringDetails.maxWidth == null || element.width <= filteringDetails.maxWidth) &&

        (filteringDetails.minHeight == null || element.height >= filteringDetails.minHeight) &&
        (filteringDetails.maxHeight == null || element.height <= filteringDetails.maxHeight) &&

        (filteringDetails.minDepth == null || element.depth >= filteringDetails.minDepth) &&
        (filteringDetails.maxDepth == null || element.depth <= filteringDetails.maxDepth) &&

        // filtering of weight section
        (filteringDetails.minWeight == null || element.weight >= filteringDetails.minWeight) &&
        (filteringDetails.maxWeight == null || element.weight <= filteringDetails.maxWeight) &&

        // filtering of source address section
        (filteringDetails.patternSourceStreet == null || element.source.street.includes(filteringDetails.patternSourceStreet)) &&
        (filteringDetails.patternSourceBuildingNumber == null || element.source.buildingNumber.includes(filteringDetails.patternSourceBuildingNumber)) &&

        (filteringDetails.patternSourceApartmentNumber == null || element.source.apartmentNumber.includes(filteringDetails.patternSourceApartmentNumber)) &&
        (filteringDetails.patternSourceCity == null || element.source.city.includes(filteringDetails.patternSourceCity)) &&

        (filteringDetails.patternSourceZipCode == null || element.source.zipCode.includes(filteringDetails.patternSourceZipCode)) &&
        (filteringDetails.patternSourceCountry == null || element.source.country.includes(filteringDetails.patternSourceCountry)) &&

        // filtering of destination address section
        (filteringDetails.patternDestinationStreet == null || element.destination.street.includes(filteringDetails.patternDestinationStreet)) &&
        (filteringDetails.patternDestinationBuildingNumber == null || element.destination.buildingNumber.includes(filteringDetails.patternDestinationBuildingNumber)) &&

        (filteringDetails.patternDestinationApartmentNumber == null || element.destination.apartmentNumber.includes(filteringDetails.patternDestinationApartmentNumber)) &&
        (filteringDetails.patternDestinationCity == null || element.destination.city.includes(filteringDetails.patternDestinationCity)) &&

        (filteringDetails.patternDestinationZipCode == null || element.destination.zipCode.includes(filteringDetails.patternDestinationZipCode)) &&
        (filteringDetails.patternDestinationCountry == null || element.destination.country.includes(filteringDetails.patternDestinationCountry)) &&

        // filtering of date of inquiring
        (filteringDetails.minDateOfInquiring == null || new Date(element.createdAt) >= new Date(filteringDetails.minDateOfInquiring)) &&
        (filteringDetails.maxDateOfInquiring == null || new Date(element.createdAt) <= new Date(filteringDetails.maxDateOfInquiring))
      );

      props.setTableData(filteredElements);
    };
    
    return (
      <React.Fragment>
        <Modal show={props.show} size="4xl" popup={true} onClose={close}>
          <Modal.Header />
          <Modal.Body style={{ overflowY: 'scroll' }}>
            <form onSubmit={submit}>
              <div className="space-y-6 px-6 pb-4 sm:pb-6 lg:px-8 xl:pb-8">
                <h1 className="mb-2 text-2xl font-bold text-gray-900 dark:text-white">
                  Filter inquiries by attributes:
                </h1>
                {error ? (
                  <Alert color="failure" icon={HiInformationCircle}>
                    <span>{error}</span>
                  </Alert>
                ) : null}
                <div className="space-y-6 gap-6" style={{ maxHeight: '70vh', paddingBottom: '20px' }}>
                  <div >

                    <SectionTitle title="Id" />
                    <IdFilterSection
                        filterData={filteringDetails}
                        handleStringChange={handleStringChange}
                    />

                    <SectionTitle title="Package dimensions" />
                    <DimensionsFilterSection
                        filterData={filteringDetails}
                        handleNumberChange={handleNumberChange}
                    />

                    <SectionTitle title="Package weight" />
                    <WeightFilterSection
                        filterData={filteringDetails}
                        handleNumberChange={handleNumberChange}
                    />

                    <SectionTitle title="Source address" />
                    <AddressFilterSection
                        prefix="Source"
                        filterData={filteringDetails}
                        handleStringChange={handleStringChange}
                    />

                    <SectionTitle title="Destination address" />
                    <AddressFilterSection
                        prefix="Destination"
                        filterData={filteringDetails}
                        handleStringChange={handleStringChange}
                    />

                    <SectionTitle title="Date of inquiring" />
                    <DateOfInquiringFilterSection
                        filterData={filteringDetails}
                        handleDateChange={handleDateChange}
                    />

                    <div className="space-y-6 w-full" style={{ display: 'flex', justifyContent: 'center' }}>
                    {isLoading ? (
                        <Button>
                        <div className="mr-3">
                            <Spinner size="sm" light={true} />
                        </div>
                        Loading ...
                        </Button>
                    ) : (
                        <Button type="submit" onClick={submit}>Submit filtering details</Button>
                    )}
                    </div>
                    <div style={{ marginBottom: '20px' }}></div>

                  </div>
                </div>
              </div>
            </form>
          </Modal.Body>
        </Modal>
      </React.Fragment>
    );
  }
  