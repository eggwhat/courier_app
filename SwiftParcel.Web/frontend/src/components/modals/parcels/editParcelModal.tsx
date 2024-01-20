import {
  Button,
  Label,
  Modal,
  Select,
  Spinner,
  TextInput,
} from "flowbite-react";
import React from "react";
import { updateParcel } from "../../../utils/api";

interface EditParcelModalProps {
  show: boolean;
  setShow: (show: boolean) => void;
  parcel: any;
  setParcel: (parcel: any) => void;
}

export function EditParcelModal(props: EditParcelModalProps) {
  const close = () => {
    setSenderName(props.parcel.senderName);
    setSenderPhone(props.parcel.senderPhone);
    setSenderAddress(props.parcel.senderAddress);
    setReceiverName(props.parcel.receiverName);
    setReceiverPhone(props.parcel.receiverPhone);
    setReceiverAddress(props.parcel.receiverAddress);
    setWeight(props.parcel.weight);
    setPrice(props.parcel.price);
    setStatus(props.parcel.status);
    props.setShow(false);
  };

  const [senderName, setSenderName] = React.useState(props.parcel.senderName);
  const [senderPhone, setSenderPhone] = React.useState(
    props.parcel.senderPhone
  );
  const [senderAddress, setSenderAddress] = React.useState(
    props.parcel.senderAddress
  );
  const [receiverName, setReceiverName] = React.useState(
    props.parcel.receiverName
  );
  const [receiverPhone, setReceiverPhone] = React.useState(
    props.parcel.receiverPhone
  );
  const [receiverAddress, setReceiverAddress] = React.useState(
    props.parcel.receiverAddress
  );
  const [weight, setWeight] = React.useState(props.parcel.weight);
  const [price, setPrice] = React.useState(props.parcel.price);
  const [status, setStatus] = React.useState(props.parcel.status);

  const [loading, setLoading] = React.useState(false);

  React.useEffect(() => {
    setSenderName(props.parcel.senderName);
    setSenderPhone(props.parcel.senderPhone);
    setSenderAddress(props.parcel.senderAddress);
    setReceiverName(props.parcel.receiverName);
    setReceiverPhone(props.parcel.receiverPhone);
    setReceiverAddress(props.parcel.receiverAddress);
    setWeight(props.parcel.weight);
    setPrice(props.parcel.price);
    setStatus(props.parcel.status);
  }, [props.parcel]);

  const onSubmit = (e: any) => {
    e.preventDefault();
    setLoading(true);
    updateParcel(props.parcel.parcelNumber, {
      senderName,
      senderPhone,
      senderAddress,
      receiverName,
      receiverPhone,
      receiverAddress,
      weight,
      price,
      status,
    })
      .then((res) => {
        if (res?.status === 200) {
          props.setParcel(res.data);
        }
      })
      .finally(() => {
        setLoading(false);
        close();
      });
  };

  return (
    <React.Fragment>
      <Modal show={props.show} size="6xl" popup={true} onClose={close}>
        <Modal.Header />
        <Modal.Body>
          <div className="space-y-6 px-6 pb-4 sm:pb-6 lg:px-8 xl:pb-8">
            <h3 className="text-xl font-medium text-gray-900 dark:text-white">
              Edit Parcel{" "}
              <span className="text-blue-700">
                {props.parcel?.parcelNumber}
              </span>
            </h3>
            <form onSubmit={onSubmit}>
              <div className="grid grid-cols-1 gap-3 md:grid-cols-3 mb-5">
                <div>
                  <div className="mb-2 block">
                    <Label htmlFor="senderName" value="Sender name" />
                  </div>
                  <TextInput
                    id="senderName"
                    required={true}
                    value={senderName}
                    onChange={(e) => setSenderName(e.target.value)}
                  />
                </div>
                <div>
                  <div className="mb-2 block">
                    <Label htmlFor="senderAddress" value="Sender address" />
                  </div>
                  <TextInput
                    id="senderAddress"
                    required={true}
                    value={senderAddress}
                    onChange={(e) => setSenderAddress(e.target.value)}
                  />
                </div>
                <div>
                  <div className="mb-2 block">
                    <Label htmlFor="senderPhone" value="Sender phone" />
                  </div>
                  <TextInput
                    id="senderPhone"
                    required={true}
                    value={senderPhone}
                    onChange={(e) => setSenderPhone(e.target.value)}
                  />
                </div>
              </div>
              <div className="grid grid-cols-1 gap-3 md:grid-cols-3 mb-5">
                <div>
                  <div className="mb-2 block">
                    <Label htmlFor="receiverName" value="Receiver name" />
                  </div>
                  <TextInput
                    id="receiverName"
                    required={true}
                    value={receiverName}
                    onChange={(e) => setReceiverName(e.target.value)}
                  />
                </div>
                <div>
                  <div className="mb-2 block">
                    <Label htmlFor="receiverAddress" value="Receiver address" />
                  </div>
                  <TextInput
                    id="receiverAddress"
                    required={true}
                    value={receiverAddress}
                    onChange={(e) => setReceiverAddress(e.target.value)}
                  />
                </div>
                <div>
                  <div className="mb-2 block">
                    <Label htmlFor="receiverPhone" value="Receiver phone" />
                  </div>
                  <TextInput
                    id="receiverPhone"
                    required={true}
                    value={receiverPhone}
                    onChange={(e) => setReceiverPhone(e.target.value)}
                  />
                </div>
              </div>
              <div className="grid grid-cols-1 gap-3 md:grid-cols-2 mb-5">
                <div>
                  <div className="mb-2 block">
                    <Label htmlFor="weight" value="Weight" />
                  </div>
                  <TextInput
                    id="weight"
                    required={true}
                    value={weight}
                    type="number"
                    step="0.1"
                    min="0.1"
                    max="1000"
                    onChange={(e) => setWeight(e.target.value)}
                  />
                </div>
                <div>
                  <div className="mb-2 block">
                    <Label htmlFor="price" value="Price" />
                  </div>
                  <TextInput
                    id="price"
                    required={true}
                    value={price}
                    type="number"
                    step="0.1"
                    min="0.1"
                    max="1000"
                    onChange={(e) => setPrice(e.target.value)}
                  />
                </div>
              </div>
              <div className="mb-5">
                <div id="select">
                  <div className="mb-2 block">
                    <Label htmlFor="status" value="Status" />
                  </div>
                  <Select
                    id="status"
                    required={true}
                    onChange={(e) => setStatus(e.target.value)}
                    value={status}
                  >
                    <option value="Pending">Pending</option>
                    <option value="In progress">In progress</option>
                    <option value="Delivered">Delivered</option>
                  </Select>
                </div>
              </div>
              <div className="w-full">
                {loading ? (
                  <Button type="submit" disabled={true}>
                    <Spinner className="w-5 h-5 mr-3 -ml-1" />
                    <span>Updating...</span>
                  </Button>
                ) : (
                  <Button type="submit">Submit</Button>
                )}
              </div>
            </form>
          </div>
        </Modal.Body>
      </Modal>
    </React.Fragment>
  );
}
