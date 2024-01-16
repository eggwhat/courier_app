import {
    Button,
    Label,
    Modal,
    TextInput,
  } from "flowbite-react";
import React from "react";
import dateFromUTCToLocal from "../../parsing/dateFromUTCToLocal";
import formatOfferStatus from "../../parsing/formatOfferStatus";
import { approvePendingOffer, cancelPendingOffer } from "../../../utils/api";
import booleanToString from "../../parsing/booleanToString";
  
  interface DeliveryDetailsModalProps {
    show: boolean;
    setShow: (show: boolean) => void;
    delivery: any;
    pageContent: string;
  }

  const formatDate = (date: string) => {
    return `${date.substring(0, 10)}`;
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

  const IdInfoDetailsSection = ({ detailsData }) => (
    <div>
        <LabelsWithBorder
            idA="id"
            valueA="Delivery id:"
            idB="id-value"
            valueB={detailsData.delivery.id}
        />
        <LabelsWithBorder
            idA="customer-id"
            valueA="Order id:"
            idB="customer-id-value"
            valueB={detailsData.delivery.orderId}
        />
    </div>
  );

  const StatusDetailsSection = ({ detailsData }) => (
    <div>
        <LabelsWithBorder
            idA="status"
            valueA="Status:"
            idB="status-value"
            valueB={formatOfferStatus(detailsData.delivery.status)}
        />
        { detailsData.delivery.status === "cannotdeliver" ?
          <div>
            <LabelsWithBorder
                idA="delivery-attempt-date"
                valueA="Delivery attempt date:"
                idB="delivery-attempt-date-value"
                valueB={formatDateToUTC(detailsData.delivery.deliveryAttemptDate)}
            />
            <LabelsWithBorder
                idA="cannot-deliver-reason"
                valueA="Cannot deliver reason:"
                idB="cannot-deliver-reason-value"
                valueB={detailsData.delivery.cannotDeliverReason}
            />
          </div>
        : null }
        <LabelsWithBorder
            idA="last-update"
            valueA="Last update:"
            idB="last-update-value"
            valueB={formatDateToUTC(detailsData.delivery.lastUpdate)}
        />
    </div>
  );

  const OrderDetailsSection = ({ detailsData }) => (
    <div>
        <LabelsWithBorder
            idA="pickup-date"
            valueA="Pickup date:"
            idB="pickup-date-value"
            valueB={formatDate(detailsData.delivery.pickupDate)}
        />
        <LabelsWithBorder
            idA="delivery-date"
            valueA="Delivery date:"
            idB="delivery-date-value"
            valueB={formatDate(detailsData.delivery.deliveryDate)}
        />
        <LabelsWithBorder
            idA="priority"
            valueA="Priority:"
            idB="priority-value"
            valueB={detailsData.delivery.priority}
        />
        <LabelsWithBorder
            idA="at-weekend"
            valueA="At weekend:"
            idB="at-weekend-value"
            valueB={booleanToString(detailsData.delivery.atWeekend)}
        />
    </div>
  );

  const PackageInfoDetailsSection = ({ detailsData }) => (
    <div>
        <LabelsWithBorder
            idA="package-volume"
            valueA="Volume:"
            idB="package-volume-value"
            valueB={detailsData.delivery.volume}
        />
        <LabelsWithBorder
            idA="package-weight"
            valueA="Weight:"
            idB="package-weight-value"
            valueB={detailsData.delivery.weight}
        />
    </div>
  );

  const AddressDetailsSection = ({ prefix, detailsData }) => (
    <div>
        <LabelsWithBorder
            idA={`${prefix}-street`}
            valueA="Street:"
            idB={`${prefix}-street-value`}
            valueB={detailsData.delivery[prefix].street}
        />
        <LabelsWithBorder
            idA={`${prefix}-building-number`}
            valueA="Building number:"
            idB={`${prefix}-building-number-value`}
            valueB={detailsData.delivery[prefix].buildingNumber}
        />
        <LabelsWithBorder
            idA={`${prefix}-apartment-number`}
            valueA="Apartment number:"
            idB={`${prefix}-apartment-number-value`}
            valueB={detailsData.delivery[prefix].apartmentNumber}
        />
        <LabelsWithBorder
            idA={`${prefix}-city`}
            valueA="City:"
            idB={`${prefix}-city-value`}
            valueB={detailsData.delivery[prefix].city}
        />
        <LabelsWithBorder
            idA={`${prefix}-zip-code`}
            valueA="Zip code:"
            idB={`${prefix}-zip-code-value`}
            valueB={detailsData.delivery[prefix].zipCode}
        />
        <LabelsWithBorder
            idA={`${prefix}-country`}
            valueA="Country:"
            idB={`${prefix}-country-value`}
            valueB={detailsData.delivery[prefix].country}
        />
    </div>
  );

  export function DeliveryDetailsModal(props: DeliveryDetailsModalProps) {
    const close = () => {
      setReason("");
      setAccepted(false);
      setRejected(false);
      setFinalized(false);
      props.setShow(false);
    };

    const submit = async (e: any) => {
      e.preventDefault();
      close();
    };
    
    const [accepted, setAccepted] = React.useState<any>(false);
    const [rejected, setRejected] = React.useState<any>(false);
    const [finalized, setFinalized] = React.useState<any>(false);

    const [reason, setReason] = React.useState<any>("");

    const accept = () => {
      approvePendingOffer(props.delivery.id);
      setFinalized(true);
    };

    const reject = (reason: string) => {
      cancelPendingOffer(props.delivery.id, reason);
      setFinalized(true);
    };

    const refresh = () => {
      close();
      window.location.reload();
    };

    return (
      <React.Fragment>
        <Modal show={props.show} size="4xl" popup={true} onClose={close}>
          <Modal.Header />
          <Modal.Body style={{ overflowY: 'scroll' }}>
            <form onSubmit={submit}>
              <div className="space-y-6 px-6 pb-4 sm:pb-6 lg:px-8 xl:pb-8">
                <h1 className="mb-2 text-2xl font-bold text-gray-900 dark:text-white">
                  Details of {props.pageContent == "offer-requests" ? 'offer request' : 'pending offer'}:
                </h1>
                <div className="space-y-6 gap-6" style={{ maxHeight: '70vh', paddingBottom: '20px' }}>
                  <div className="space-y-6 gap-6">

                    <SectionTitle title="Id info" />
                    <IdInfoDetailsSection
                        detailsData={props}
                    />

                    <SectionTitle title="Status info" />
                    <StatusDetailsSection
                        detailsData={props}
                    />

                    <SectionTitle title="Order info" />
                    <OrderDetailsSection
                        detailsData={props}
                    />

                    <SectionTitle title="Package info" />
                    <PackageInfoDetailsSection
                        detailsData={props}
                    />

                    <SectionTitle title="Source address" />
                    <AddressDetailsSection
                        prefix="source"
                        detailsData={props}
                    />

                    <SectionTitle title="Destination address" />
                    <AddressDetailsSection
                        prefix="destination"
                        detailsData={props}
                    />

                    { props.pageContent == "pending-offers" ? (
                      <div className="mb-4 border-b border-gray-200 pb-1 grid grid-cols-1 md:grid-cols-2 gap-4">
                        <Button onClick={() => {setAccepted(true); setRejected(false);}}>Accept</Button>
                        <Button onClick={() => {setAccepted(false); setRejected(true);}}>Reject</Button>
                      </div>
                    ) : null }

                    { (accepted && !finalized) ? (
                      <div className="mb-4 pb-1 grid grid-cols-1 md:grid-cols-1 gap-4">
                        <Button onClick={() => accept()}>Confirm acceptation</Button>
                      </div>
                    ) : null }

                    { (rejected && !finalized) ? (
                      <div className="mb-4 pb-1 grid grid-cols-1 md:grid-cols-1 gap-4">
                        <Label htmlFor="reason-of-rejection"  className="mb-2 block text-sm font-medium text-gray-700">
                            Input reason of rejection:
                          </Label>
                          <TextInput 
                            id="reason-of-rejection" 
                            type="string"
                            lang="en"
                            value={reason}
                            onChange={(e) => setReason(e.target.value)}
                            className={`border-gray-300 focus:ring-blue-500 focus:border-blue-500 block w-full shadow-sm sm:text-sm rounded-md`}
                          />
                        <Button onClick={() => reject(reason)}>Confirm rejection</Button>
                      </div>
                    ) : null }

                    { finalized ? (
                      <div className="mb-4 pb-1 grid grid-cols-1 md:grid-cols-1 gap-4">
                        <Button onClick={() => refresh()}>Refresh page</Button>
                      </div>
                    ) : null }

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
  