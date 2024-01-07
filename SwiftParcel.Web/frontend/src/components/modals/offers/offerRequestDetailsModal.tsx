import {
    Label,
    Modal,
  } from "flowbite-react";
  import React from "react";
  import booleanToString from "../../parsing/booleanToString";
  import dateFromUTCToLocal from "../../parsing/dateFromUTCToLocal";
  
  interface OfferRequestDetailsModalProps {
    show: boolean;
    setShow: (show: boolean) => void;
    offerRequest: any;
  }

  const formatDate = (date: string) => {
    return `${date.substring(0, 10)}  ${date.substring(11, 19)}`;
  };

  const formatDateToUTC = (date: string) => {
    const utcDate = dateFromUTCToLocal(date);
    return `${utcDate.substring(0, 10)}  ${utcDate.substring(11, 19)}`;
  };

  const LabelsWithBorder = ({ idA, valueA, idB, valueB }) => (
    <div className="mb-4 border-b border-gray-200 pb-1 grid grid-cols-1 md:grid-cols-2 gap-4">
      <Label
          id={idA}
          value={valueA}
      />
      <Label
          id={idB}
          value={valueB}
      />
    </div>
  );

  const SectionTitle = ({ title }) => (
    <div className="mb-4 border-b border-gray-400 pb-1">
      <h2 className="text-xl font-semibold text-gray-800">{title}</h2>
    </div>
  );

  const BasicInfoDetailsSection = ({ detailsData }) => (
    <div>
        <LabelsWithBorder
            idA="id"
            valueA="Offer request's id:"
            idB="id-value"
            valueB={detailsData.offerRequest.id}
        />
        <LabelsWithBorder
            idA="customer-id"
            valueA="Customer's id:"
            idB="customer-id-value"
            valueB={detailsData.offerRequest.customerId}
        />
    </div>
  );

  const StatusDetailsSection = ({ detailsData }) => (
    <div>
        <LabelsWithBorder
            idA="status"
            valueA="Status:"
            idB="status-value"
            valueB={detailsData.offerRequest.status}
        />
        <LabelsWithBorder
            idA="order-request-date"
            valueA="Order request date:"
            idB="order-request-date-value"
            valueB={formatDate(detailsData.offerRequest.orderRequestDate)}
        />
        <LabelsWithBorder
            idA="request-valid-to"
            valueA="Request valid to:"
            idB="request-valid-to-value"
            valueB={formatDate(detailsData.offerRequest.requestValidTo)}
        />
    </div>
  );

  const BuyerInfoDetailsSection = ({ detailsData }) => (
    <div>
        <LabelsWithBorder
            idA="buyerName"
            valueA="Buyer name:"
            idB="buyerName-value"
            valueB={detailsData.offerRequest.buyerName}
        />
        <LabelsWithBorder
            idA="buyerEmail"
            valueA="Buyer email:"
            idB="buyerEmail-value"
            valueB={detailsData.offerRequest.buyerEmail}
        />
    </div>
  );

  const AddressDetailsSection = ({ prefix, detailsData }) => (
    <div>
        <LabelsWithBorder
            idA={`${prefix}-street`}
            valueA="Street:"
            idB={`${prefix}-street-value`}
            valueB={detailsData.offerRequest[prefix].street}
        />
        <LabelsWithBorder
            idA={`${prefix}-building-number`}
            valueA="Building number:"
            idB={`${prefix}-building-number-value`}
            valueB={detailsData.offerRequest[prefix].buildingNumber}
        />
        <LabelsWithBorder
            idA={`${prefix}-apartment-number`}
            valueA="Apartment number:"
            idB={`${prefix}-apartment-number-value`}
            valueB={detailsData.offerRequest[prefix].apartmentNumber}
        />
        <LabelsWithBorder
            idA={`${prefix}-city`}
            valueA="City:"
            idB={`${prefix}-city-value`}
            valueB={detailsData.offerRequest[prefix].city}
        />
        <LabelsWithBorder
            idA={`${prefix}-zipCode`}
            valueA="Zip code:"
            idB={`${prefix}-zip-code-value`}
            valueB={detailsData.offerRequest[prefix].zipCode}
        />
        <LabelsWithBorder
            idA={`${prefix}-country`}
            valueA="Country:"
            idB={`${prefix}-country-value`}
            valueB={detailsData.offerRequest[prefix].country}
        />
    </div>
  );

  const AdditionalInfoDetailsSection = ({ detailsData }) => (
    <div>
        <LabelsWithBorder
            idA="decision-date"
            valueA="Decision date:"
            idB="decision-date-value"
            valueB={formatDateToUTC(detailsData.offerRequest.decisionDate)}
        />
        <LabelsWithBorder
            idA="pickedUpAt"
            valueA="Picked up at:"
            idB="pickedUpAt-value"
            valueB={formatDateToUTC(detailsData.offerRequest.pickedUpAt)}
        />
        <LabelsWithBorder
            idA="deliveredAt-date"
            valueA="Delivered at:"
            idB="deliveredAt-date-value"
            valueB={formatDateToUTC(detailsData.offerRequest.deliveredAt)}
        />
        <LabelsWithBorder
            idA="cannotDeliverAt-date"
            valueA="Cannot deliver at:"
            idB="cannotDeliverAt-date-value"
            valueB={formatDateToUTC(detailsData.offerRequest.cannotDeliverAt)}
        />
        <LabelsWithBorder
            idA="cancellationReason"
            valueA="Cancellation reason:"
            idB="cancellationReason-value"
            valueB={detailsData.offerRequest.cancellationReason}
        />
        <LabelsWithBorder
            idA="cannotDeliverReason"
            valueA="Cannot deliver reason:"
            idB="cannotDeliverReason-value"
            valueB={detailsData.offerRequest.cannotDeliverReason}
        />
    </div>
  );

  export function OfferRequestDetailsModal(props: OfferRequestDetailsModalProps) {
    const close = () => {
      props.setShow(false);
    };

    const submit = async (e: any) => {
      e.preventDefault();
      close();
    };
    
    return (
      <React.Fragment>
        <Modal show={props.show} size="4xl" popup={true} onClose={close}>
          <Modal.Header />
          <Modal.Body style={{ overflowY: 'scroll' }}>
            <form onSubmit={submit}>
              <div className="space-y-6 px-6 pb-4 sm:pb-6 lg:px-8 xl:pb-8">
                <h1 className="mb-2 text-2xl font-bold text-gray-900 dark:text-white">
                  Details of offer request:
                </h1>
                <div className="space-y-6 gap-6" style={{ maxHeight: '70vh', paddingBottom: '20px' }}>
                  <div className="space-y-6 gap-6">

                    <SectionTitle title="Basic info" />
                    <BasicInfoDetailsSection
                        detailsData={props}
                    />

                    <SectionTitle title="Status info" />
                    <StatusDetailsSection
                        detailsData={props}
                    />

                    <SectionTitle title="Buyer info" />
                    <BuyerInfoDetailsSection
                        detailsData={props}
                    />

                    <SectionTitle title="Buyer address" />
                    <AddressDetailsSection
                        prefix="buyerAddress"
                        detailsData={props}
                    />

                    <SectionTitle title="Additional info" />
                    <AdditionalInfoDetailsSection
                        detailsData={props}
                    />

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
  