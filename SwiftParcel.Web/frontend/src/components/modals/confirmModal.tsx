import { Button, Modal } from "flowbite-react";
import React from "react";
import { HiOutlineExclamationCircle } from "react-icons/hi";

interface ConfirmModalProps {
  show: boolean;
  setShow: (show: boolean) => void;
  onConfirm: () => void;
  message: string;
  confirmText: string;
  cancelText: string;
  confirmColor?: string;
  cancelColor?: string;
}

export function ConfirmModal(props: ConfirmModalProps) {
  const close = () => {
    props.setShow(false);
  };

  return (
    <React.Fragment>
      <Modal show={props.show} size="md" popup={true} onClose={close}>
        <Modal.Header />
        <Modal.Body>
          <div className="text-center">
            <HiOutlineExclamationCircle className="mx-auto mb-4 h-14 w-14 text-gray-400 dark:text-gray-200" />
            <h3 className="mb-5 text-lg font-normal text-gray-500 dark:text-gray-400">
              {props.message}
            </h3>
            <div className="flex justify-center gap-4">
              <Button
                color={props.confirmColor || "failure"}
                onClick={props.onConfirm}
              >
                {props.confirmText}
              </Button>
              <Button color={props.cancelColor || "gray"} onClick={close}>
                {props.cancelText}
              </Button>
            </div>
          </div>
        </Modal.Body>
      </Modal>
    </React.Fragment>
  );
}
