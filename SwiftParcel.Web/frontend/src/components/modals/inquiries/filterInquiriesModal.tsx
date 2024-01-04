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
  import { isPackageValid } from "../../details/inquiry";
  
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
    filterStatus: string;
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

  const StatusFilterSection = ({ filterData, handleStringChange }) => (
    <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
        <select
            id="filter-status"
            value={filterData.filterStatus}
            onChange={handleStringChange('filterStatus')}
            className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-300 focus:ring focus:ring-indigo-200 focus:ring-opacity-50"
        >
            <option value="all">all</option>
            <option value="expired">expired</option>
            <option value="valid">valid</option>
        </select>
    </div>
  );

  export function FilterInquiriesModal(props: FilterInquiriesModalProps) {
    const close = () => {
      setError("");
      setIsLoading(false);
      props.setShow(false);
    };

    const minDefNum = 0;
    const maxDefNum = 99999;

    const [filteringDetails, setFilteringDetails] = React.useState<FilteringDetails>({
        patternId: "",
        minWidth: minDefNum,
        maxWidth: maxDefNum,
        minHeight: minDefNum,
        maxHeight: maxDefNum,
        minDepth: minDefNum,
        maxDepth: maxDefNum,
        minWeight: minDefNum,
        maxWeight: maxDefNum,
        patternSourceStreet: "",
        patternSourceBuildingNumber: "",
        patternSourceApartmentNumber: "",
        patternSourceCity: "",
        patternSourceZipCode: "",
        patternSourceCountry: "",
        patternDestinationStreet: "",
        patternDestinationBuildingNumber: "",
        patternDestinationApartmentNumber: "",
        patternDestinationCity: "",
        patternDestinationZipCode: "",
        patternDestinationCountry: "",
        minDateOfInquiring: "",
        maxDateOfInquiring: "",
        filterStatus: "all"
    });

    const handleNumberChange = <T extends keyof FilteringDetails>(field: T) => (event: React.ChangeEvent<HTMLInputElement>) => {
        const newValue = parseFloat(event.target.value);
        setFilteringDetails(prevState => ({
            ...prevState,
            [field]: isNaN(newValue) ? 0 : newValue
        }));
    };

    const handleStringChange = <T extends keyof FilteringDetails>(field: T) => (event: React.ChangeEvent<HTMLInputElement>) => {
        const newValue = event.target.value;
        setFilteringDetails(prevState => ({
            ...prevState,
            [field]: newValue
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

    const clearDetails = () => {
        setFilteringDetails({
            patternId: "",
            minWidth: minDefNum,
            maxWidth: maxDefNum,
            minHeight: minDefNum,
            maxHeight: maxDefNum,
            minDepth: minDefNum,
            maxDepth: maxDefNum,
            minWeight: minDefNum,
            maxWeight: maxDefNum,
            patternSourceStreet: "",
            patternSourceBuildingNumber: "",
            patternSourceApartmentNumber: "",
            patternSourceCity: "",
            patternSourceZipCode: "",
            patternSourceCountry: "",
            patternDestinationStreet: "",
            patternDestinationBuildingNumber: "",
            patternDestinationApartmentNumber: "",
            patternDestinationCity: "",
            patternDestinationZipCode: "",
            patternDestinationCountry: "",
            minDateOfInquiring: "",
            maxDateOfInquiring: "",
            filterStatus: "all"
        });
    };

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
        (filteringDetails.patternId == "" || element.id.includes(filteringDetails.patternId)) &&

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
        (filteringDetails.patternSourceStreet == "" || element.source.street.includes(filteringDetails.patternSourceStreet)) &&
        (filteringDetails.patternSourceBuildingNumber == "" || element.source.buildingNumber.includes(filteringDetails.patternSourceBuildingNumber)) &&

        (filteringDetails.patternSourceApartmentNumber == "" || element.source.apartmentNumber.includes(filteringDetails.patternSourceApartmentNumber)) &&
        (filteringDetails.patternSourceCity == "" || element.source.city.includes(filteringDetails.patternSourceCity)) &&

        (filteringDetails.patternSourceZipCode == "" || element.source.zipCode.includes(filteringDetails.patternSourceZipCode)) &&
        (filteringDetails.patternSourceCountry == "" || element.source.country.includes(filteringDetails.patternSourceCountry)) &&

        // filtering of destination address section
        (filteringDetails.patternDestinationStreet == "" || element.destination.street.includes(filteringDetails.patternDestinationStreet)) &&
        (filteringDetails.patternDestinationBuildingNumber == "" || element.destination.buildingNumber.includes(filteringDetails.patternDestinationBuildingNumber)) &&

        (filteringDetails.patternDestinationApartmentNumber == "" || element.destination.apartmentNumber.includes(filteringDetails.patternDestinationApartmentNumber)) &&
        (filteringDetails.patternDestinationCity == "" || element.destination.city.includes(filteringDetails.patternDestinationCity)) &&

        (filteringDetails.patternDestinationZipCode == "" || element.destination.zipCode.includes(filteringDetails.patternDestinationZipCode)) &&
        (filteringDetails.patternDestinationCountry == "" || element.destination.country.includes(filteringDetails.patternDestinationCountry)) &&

        // filtering of date of inquiring
        (filteringDetails.minDateOfInquiring == "" || new Date(element.createdAt) >= new Date(filteringDetails.minDateOfInquiring)) &&
        (filteringDetails.maxDateOfInquiring == "" || new Date(element.createdAt) <= new Date(filteringDetails.maxDateOfInquiring)) &&

        // filtering of status
        (filteringDetails.filterStatus != "expired" || isPackageValid(element.validTo) == false) &&
        (filteringDetails.filterStatus != "valid" || isPackageValid(element.validTo) == true)
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
                  <div>

                    <div className="space-y-6 w-full" style={{ display: 'flex', justifyContent: 'center' }}>
                        <Button onClick={clearDetails}>Clear filtering details</Button>
                    </div>

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

                    <SectionTitle title="Status" />
                    <StatusFilterSection
                        filterData={filteringDetails}
                        handleStringChange={handleStringChange}
                    />

                    <div style={{ marginBottom: '20px' }}></div>

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
  