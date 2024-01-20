import { Button, Label, Modal, Spinner, TextInput } from "flowbite-react";
import React from "react";
import { createCourier } from "../../../utils/api";

interface CreateCourierModalProps {
  show: boolean;
  setShow: (show: boolean) => void;
  toggleRender: boolean;
  setToggleRender: (toggleRender: boolean) => void;
}

export function CreateCourierModal(props: CreateCourierModalProps) {
  const close = () => {
    props.setShow(false);
  };

  const [firstname, setFirstname] = React.useState("");
  const [lastname, setLastname] = React.useState("");
  const [phone, setPhone] = React.useState("");

  const [loading, setLoading] = React.useState(false);

  const onSubmit = (e: any) => {
    e.preventDefault();
    setLoading(true);
    createCourier({ firstname, lastname, phone })
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
              Create Courier
            </h3>
            <form onSubmit={onSubmit}>
              <div className="grid grid-cols-1 gap-3 md:grid-cols-2 mb-5">
                <div>
                  <div className="mb-2 block">
                    <Label htmlFor="firstname" value="Firstname" />
                  </div>
                  <TextInput
                    id="firstname"
                    required={true}
                    value={firstname}
                    onChange={(e) => setFirstname(e.target.value)}
                  />
                </div>
                <div>
                  <div className="mb-2 block">
                    <Label htmlFor="lastname" value="Lastname" />
                  </div>
                  <TextInput
                    id="lastname"
                    required={true}
                    value={lastname}
                    onChange={(e) => setLastname(e.target.value)}
                  />
                </div>
              </div>
              <div className="grid grid-cols-1 mb-5">
                <div>
                  <div className="mb-2 block">
                    <Label htmlFor="phone" value="Phone number" />
                  </div>
                  <TextInput
                    id="phone"
                    type="tel"
                    pattern="(86[0-9]{7}|\+3706[0-9]{7})"
                    required={true}
                    value={phone}
                    onChange={(e) => setPhone(e.target.value)}
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
