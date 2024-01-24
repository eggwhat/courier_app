import {
    Button,
    Label,
    Modal,
    TextInput,
  } from "flowbite-react";
  import React from "react";
  import formatOfferStatus from "../../parsing/formatOfferStatus";
  
  interface OrderStatusModalProps {
    show: boolean;
    setShow: (show: boolean) => void;
    orderStatus: any;
  }

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

  const InfoSection = ({ detailsData }) => (
    <div>
        <LabelsWithBorder
            idA="id"
            valueA="Order id:"
            idB="id-value"
            valueB={detailsData.orderStatus.orderId}
        />
        <LabelsWithBorder
            idA="status"
            valueA="Status:"
            idB="status-value"
            valueB={formatOfferStatus(detailsData.orderStatus.status)}
        />
    </div>
  );

  export function OrderStatusModal(props: OrderStatusModalProps) {
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
                  Status of your order:
                </h1>
                <div className="space-y-6 gap-6" style={{ maxHeight: '70vh', paddingBottom: '20px' }}>
                  <div className="space-y-6 gap-6">
                    
                    <div style={{ marginBottom: '40px' }}></div>

                    <InfoSection
                        detailsData={props}
                    />

                  </div>
                </div>
              </div>
            </form>
          </Modal.Body>
        </Modal>
      </React.Fragment>
    );
  }
  