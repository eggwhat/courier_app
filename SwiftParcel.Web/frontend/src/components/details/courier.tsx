import { Table, Button, Tooltip, Spinner } from "flowbite-react";
import React from "react";
import { BsJournalMinus, BsPencil, BsTrash } from "react-icons/bs";
import { ConfirmModal } from "../modals/confirmModal";
import {
  deleteCourier as apiDeleteCourier,
  updateCourier,
  updateParcel,
} from "../../utils/api";
import { EditCourierModal } from "../modals/couriers/editCourierModal";
import { AssignCarToCourierModal } from "../modals/couriers/assignCarToCourierModal";
import { AssignUserToCourierModal } from "../modals/couriers/assignUserToCourierModal";

interface CourierDetailsProps {
  courierData: any;
  toggleRender: boolean | undefined;
  setToggleRender: ((toggleRender: boolean) => void) | undefined;
  showEditDeleteBtn: boolean | undefined;
  showUser: boolean | undefined;
  showCar: boolean | undefined;
  showAssignParcelBtn: string | undefined;
  setModalOpen: ((modelOpen: boolean) => void) | undefined;
  setParcel: ((parcel: any) => void) | undefined;
}

export function CourierDetails({
  courierData,
  toggleRender,
  setToggleRender,
  showEditDeleteBtn,
  showUser,
  showCar,
  showAssignParcelBtn,
  setModalOpen,
  setParcel,
}: CourierDetailsProps) {
  const [showDeleteModal, setShowDeleteModal] = React.useState(false);
  const [showEditModal, setShowEditModal] = React.useState(false);

  const [showUnassignModal1, setShowUnassignModal1] = React.useState(false);
  const [showUnassignModal2, setShowUnassignModal2] = React.useState(false);

  const [showAssignModal1, setShowAssignModal1] = React.useState(false);
  const [showAssignModal2, setShowAssignModal2] = React.useState(false);

  const [loadingDelete, setLoadingDelete] = React.useState(false);
  const [loadingAssign, setLoadingAssign] = React.useState(false);
  const [loadingAssign1, setLoadingAssign1] = React.useState(false);
  const [loadingAssign2, setLoadingAssign2] = React.useState(false);

  const [courier, setCourier] = React.useState<any>(courierData);

  const editCourier = () => {
    if (!showEditDeleteBtn) return;
    setShowEditModal(true);
  };

  const showDeleteCourierModal = () => {
    if (!showEditDeleteBtn) return;
    setShowDeleteModal(true);
  };

  const showUnassignUserModal = () => {
    if (!showEditDeleteBtn) return;
    setShowUnassignModal1(true);
  };

  const showUnassignCarModal = () => {
    if (!showEditDeleteBtn) return;
    setShowUnassignModal2(true);
  };

  const deleteCourier = () => {
    if (!showEditDeleteBtn) return;
    setShowDeleteModal(false);
    setLoadingDelete(true);
    apiDeleteCourier(courier.id)
      .then((res) => {
        if (res?.status === 204) {
          if (setToggleRender !== undefined) setToggleRender(!toggleRender);
        }
      })
      .finally(() => {
        setLoadingDelete(false);
      });
  };

  const unassignUser = () => {
    if (!showEditDeleteBtn) return;
    setShowUnassignModal1(false);
    setLoadingAssign1(true);
    updateCourier(courier.id, { userId: null })
      .then((res) => {
        if (res?.status === 200) {
          setCourier(res.data);
        }
      })
      .finally(() => {
        if (setToggleRender !== undefined) setToggleRender(!toggleRender);
        setLoadingAssign1(false);
      });
  };

  const unassignCar = () => {
    if (!showEditDeleteBtn) return;
    setShowUnassignModal2(false);
    setLoadingAssign2(true);
    updateCourier(courier.id, { carId: null })
      .then((res) => {
        if (res?.status === 200) {
          setCourier(res.data);
        }
      })
      .finally(() => {
        if (setToggleRender !== undefined) setToggleRender(!toggleRender);
        setLoadingAssign2(false);
      });
  };

  const assignToParcel = () => {
    if (!showAssignParcelBtn) return;
    setLoadingAssign(true);

    updateParcel(showAssignParcelBtn, { courierId: courier.id })
      .then((res) => {
        if (res?.status === 200) {
          if (setParcel !== undefined) setParcel(res.data);
          if (setToggleRender !== undefined) setToggleRender(!toggleRender);
          if (setModalOpen !== undefined) setModalOpen(false);
        }
      })
      .finally(() => {
        setLoadingAssign(false);
      });
  };

  return (
    <>
      <Table.Row
        className="bg-white dark:border-gray-700 dark:bg-gray-800"
        key={courier.id}
      >
        <Table.Cell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
          {courier.firstname}
        </Table.Cell>
        <Table.Cell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
          {courier.lastname}
        </Table.Cell>
        <Table.Cell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
          <a href={`tel:${courier.phone}`} className="hover:underline">
            {courier.phone}
          </a>
        </Table.Cell>
        {showUser ? (
          <Table.Cell>
            {courier.user ? (
              <span>{courier.user?.username}</span>
            ) : (
              <>
                <Tooltip content="Click to assign">
                  <b
                    className="cursor-pointer"
                    onClick={() => setShowAssignModal1(true)}
                  >
                    None
                  </b>
                </Tooltip>
                <AssignUserToCourierModal
                  show={showAssignModal1}
                  setShow={setShowAssignModal1}
                  courier={courier}
                  setCourier={setCourier}
                  toggleRender={toggleRender}
                  setToggleRender={setToggleRender}
                />
              </>
            )}
          </Table.Cell>
        ) : null}
        {showCar ? (
          <Table.Cell>
            {courier.car ? (
              <span>{courier.car?.licensePlate}</span>
            ) : (
              <>
                <Tooltip content="Click to assign">
                  <b
                    className="cursor-pointer"
                    onClick={() => setShowAssignModal2(true)}
                  >
                    None
                  </b>
                </Tooltip>
                <AssignCarToCourierModal
                  show={showAssignModal2}
                  setShow={setShowAssignModal2}
                  courier={courier}
                  setCourier={setCourier}
                  setToggleRender={setToggleRender}
                  toggleRender={toggleRender}
                />
              </>
            )}
          </Table.Cell>
        ) : null}
        {showEditDeleteBtn ? (
          <Table.Cell>
            <span className="flex flex-wrap gap-2">
              <Button.Group outline={true}>
                <Button size="sm" onClick={editCourier}>
                  <Tooltip content="Edit Courier">
                    <BsPencil className="h-4 w-4" />
                  </Tooltip>
                </Button>
                {loadingDelete ? (
                  <Button size="sm" color="failure">
                    <Spinner size="sm" className="h-4 w-4" light={true} />
                  </Button>
                ) : (
                  <Button
                    size="sm"
                    color="failure"
                    onClick={showDeleteCourierModal}
                  >
                    <Tooltip content="Delete Courier">
                      <BsTrash className="h-4 w-4" />
                    </Tooltip>
                  </Button>
                )}
                {loadingAssign1 ? (
                  <Button size="sm" color="purple">
                    <Spinner size="sm" className="h-4 w-4" light={true} />
                  </Button>
                ) : (
                  <Button
                    size="sm"
                    color="purple"
                    onClick={showUnassignUserModal}
                    disabled={courier.user == null}
                  >
                    <Tooltip content="Unassign User">
                      <BsJournalMinus className="h-4 w-4" />
                    </Tooltip>
                  </Button>
                )}
                {loadingAssign2 ? (
                  <Button size="sm" color="dark">
                    <Spinner size="sm" className="h-4 w-4" light={true} />
                  </Button>
                ) : (
                  <Button
                    size="sm"
                    color="dark"
                    onClick={showUnassignCarModal}
                    disabled={courier.car == null}
                  >
                    <Tooltip content="Unassign Car">
                      <BsJournalMinus className="h-4 w-4" />
                    </Tooltip>
                  </Button>
                )}
              </Button.Group>
            </span>
          </Table.Cell>
        ) : null}
        {showAssignParcelBtn ? (
          loadingAssign ? (
            <Table.Cell>
              <Button>
                <Spinner size="sm" light={true} className="mr-3" />
                <span>Assigning...</span>
              </Button>
            </Table.Cell>
          ) : (
            <Table.Cell>
              <Button onClick={assignToParcel}>Assign</Button>
            </Table.Cell>
          )
        ) : null}
      </Table.Row>

      {showEditDeleteBtn ? (
        <>
          <ConfirmModal
            show={showDeleteModal}
            setShow={setShowDeleteModal}
            onConfirm={deleteCourier}
            message={`Are you sure you want to delete ${courier.firstname} ${courier.lastname}?`}
            confirmText={"Yes, I'm sure"}
            cancelText={"No, cancel"}
          />
          <ConfirmModal
            show={showUnassignModal1}
            setShow={setShowUnassignModal1}
            onConfirm={unassignUser}
            message={`Are you sure you want to unassign user from courier ${courier.firstname} ${courier.lastname}?`}
            confirmText={"Yes, I'm sure"}
            cancelText={"No, cancel"}
            confirmColor="purple"
          />
          <ConfirmModal
            show={showUnassignModal2}
            setShow={setShowUnassignModal2}
            onConfirm={unassignCar}
            message={`Are you sure you want to unassign car from courier ${courier.firstname} ${courier.lastname}?`}
            confirmText={"Yes, I'm sure"}
            cancelText={"No, cancel"}
            confirmColor="dark"
          />
          <EditCourierModal
            show={showEditModal}
            setShow={setShowEditModal}
            courier={courier}
            setCourier={setCourier}
          />
        </>
      ) : null}
    </>
  );
}
