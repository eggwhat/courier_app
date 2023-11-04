import { Button, Label, Modal, Spinner, TextInput } from "flowbite-react";
import React from "react";
import { createCar } from "../../../utils/api";

interface CreateCarModalProps {
  show: boolean;
  setShow: (show: boolean) => void;
  toggleRender: boolean;
  setToggleRender: (toggleRender: boolean) => void;
}

export function CreateCarModal(props: CreateCarModalProps) {
  const close = () => {
    props.setShow(false);
  };

  const [make, setMake] = React.useState("");
  const [model, setModel] = React.useState("");
  const [licensePlate, setLicensePlate] = React.useState("");

  const [loading, setLoading] = React.useState(false);

  const onSubmit = (e: any) => {
    e.preventDefault();
    setLoading(true);
    createCar({ make, model, licensePlate })
      .then((res) => {
        if (res?.status === 201) {
          props.setToggleRender(!props.toggleRender);
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
              Create Car
            </h3>
            <form onSubmit={onSubmit}>
              <div className="grid grid-cols-1 gap-3 md:grid-cols-2 mb-5">
                <div>
                  <div className="mb-2 block">
                    <Label htmlFor="make" value="Make" />
                  </div>
                  <TextInput
                    id="make"
                    required={true}
                    value={make}
                    onChange={(e) => setMake(e.target.value)}
                  />
                </div>
                <div>
                  <div className="mb-2 block">
                    <Label htmlFor="model" value="Model" />
                  </div>
                  <TextInput
                    id="model"
                    required={true}
                    value={model}
                    onChange={(e) => setModel(e.target.value)}
                  />
                </div>
              </div>
              <div className="grid grid-cols-1 mb-5">
                <div>
                  <div className="mb-2 block">
                    <Label htmlFor="licensePlate" value="License Plate" />
                  </div>
                  <TextInput
                    id="licensePlate"
                    required={true}
                    value={licensePlate}
                    onChange={(e) => setLicensePlate(e.target.value)}
                  />
                </div>
              </div>
              <div className="w-full">
                {loading ? (
                  <Button type="submit" disabled={true}>
                    <Spinner className="w-5 h-5 mr-3 -ml-1" />
                    <span>Creating...</span>
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
