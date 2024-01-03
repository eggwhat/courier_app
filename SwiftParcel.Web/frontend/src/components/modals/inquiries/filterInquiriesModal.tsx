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
    minWidth: number;
    maxWidth: number;
    minHeight: number;
    maxHeight: number;
    minDepth: number;
    maxDepth: number;
    minWeight: number;
    maxWeight: number;
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

  const SectionTitle = ({ title }) => (
    <div className="mb-4 border-b border-gray-300 pb-1">
      <h2 className="text-xl font-semibold text-gray-800">{title}</h2>
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

  export function FilterInquiriesModal(props: FilterInquiriesModalProps) {
    const close = () => {
      setError("");
      setIsLoading(false);
      props.setShow(false);
    };

    const [filteringDetails, setFilteringDetails] = React.useState<FilteringDetails>({
        minWidth: null,
        maxWidth: null,
        minHeight: null,
        maxHeight: null,
        minDepth: null,
        maxDepth: null,
        minWeight: null,
        maxWeight: null
    });

    const handleNumberChange = <T extends keyof FilteringDetails>(field: T) => (event: React.ChangeEvent<HTMLInputElement>) => {
        const newValue = parseFloat(event.target.value);
        setFilteringDetails(prevState => ({
            ...prevState,
            [field]: isNaN(newValue) ? null : newValue
        }));
        console.log(filteringDetails);
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
        // filtering of dimensions section
        (filteringDetails.minWidth == null || element.width >= filteringDetails.minWidth) &&
        (filteringDetails.maxWidth == null || element.width <= filteringDetails.maxWidth) &&

        (filteringDetails.minHeight == null || element.height >= filteringDetails.minHeight) &&
        (filteringDetails.maxHeight == null || element.height <= filteringDetails.maxHeight) &&

        (filteringDetails.minDepth == null || element.depth >= filteringDetails.minDepth) &&
        (filteringDetails.maxDepth == null || element.depth <= filteringDetails.maxDepth) &&

        // filtering of weight section
        (filteringDetails.minWeight == null || element.weight >= filteringDetails.minWeight) &&
        (filteringDetails.maxWeight == null || element.weight <= filteringDetails.maxWeight)
      );

      props.setTableData(filteredElements);
    };
    
    return (
      <React.Fragment>
        <Modal show={props.show} size="4xl" popup={true} onClose={close}>
          <Modal.Header />
          <Modal.Body>
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
                <div className="gap-6">
                  <div >

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

                  </div>
                </div>

                <div className="w-full">
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
              </div>
            </form>
          </Modal.Body>
        </Modal>
      </React.Fragment>
    );
  }
  