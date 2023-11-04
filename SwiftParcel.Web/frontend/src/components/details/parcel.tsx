import { Table, Badge, Button, Tooltip, Spinner } from "flowbite-react";
import React from "react";
import { BsJournalMinus, BsPencil, BsTrash } from "react-icons/bs";
import { Link, useNavigate } from "react-router-dom";
import { ConfirmModal } from "../modals/confirmModal";
import { deleteParcel as apiDeleteParcel, updateParcel } from "../../utils/api";
import { EditParcelModal } from "../modals/parcels/editParcelModal";
import { AssignCourierToParcelModal } from "../modals/parcels/assignCourierToParcelModal";

interface ParcelDetailsProps {
  parcelData: any;
  toggleRender: boolean | undefined;
  setToggleRender: ((toggleRender: boolean) => void) | undefined;
  showEditDeleteBtn: boolean | undefined;
  showCourier: boolean | undefined;
  showDeliverBtn: boolean | undefined;
  showAssignBtn: number | undefined;
}

export function ParcelDetails({
  parcelData,
  toggleRender,
  setToggleRender,
  showEditDeleteBtn,
  showCourier,
  showDeliverBtn,
  showAssignBtn,
}: ParcelDetailsProps) {
  const [showDeleteModal, setShowDeleteModal] = React.useState(false);
  const [showEditModal, setShowEditModal] = React.useState(false);
  const [showUnassignModal, setShowUnassignModal] = React.useState(false);
  const [showAssignCourierModal, setShowAssignCourierModal] =
    React.useState(false);
  const [loadingDelete, setLoadingDelete] = React.useState(false);
  const [loadingAssign, setLoadingAssign] = React.useState(false);

  const [parcel, setParcel] = React.useState<any>(parcelData);

  const navigate = useNavigate();

  const editParcel = () => {
    if (!showEditDeleteBtn) return;
    setShowEditModal(true);
  };

  const showDeleteParcelModal = () => {
    if (!showEditDeleteBtn) return;
    setShowDeleteModal(true);
  };

  const showUnassignParcelModal = () => {
    if (!showEditDeleteBtn) return;
    setShowUnassignModal(true);
  };

  const deleteParcel = () => {
    if (!showEditDeleteBtn) return;
    setShowDeleteModal(false);
    setLoadingDelete(true);
    apiDeleteParcel(parcel.parcelNumber)
      .then((res) => {
        if (res?.status === 204) {
          if (setToggleRender !== undefined) setToggleRender(!toggleRender);
        }
      })
      .finally(() => {
        setLoadingDelete(false);
      });
  };

  const unassignParcel = () => {
    if (!showEditDeleteBtn) return;
    setShowUnassignModal(false);
    setLoadingAssign(true);
    updateParcel(parcel.parcelNumber, { courierId: null })
      .then((res) => {
        if (res?.status === 200) {
          setParcel(res.data);
        }
      })
      .finally(() => {
        if (setToggleRender !== undefined) setToggleRender(!toggleRender);
        setLoadingAssign(false);
      });
  };

  const setToDeliver = () => {
    if (!showDeliverBtn) return;
    localStorage.setItem("parcelId", parcel.parcelNumber);
    navigate("/deliveries");
  };

  const assignToCourier = () => {
    if (!showAssignBtn) return;
    setLoadingAssign(true);
    updateParcel(parcel.parcelNumber, { courierId: showAssignBtn })
      .then((res) => {
        if (res?.status === 200) {
          setParcel(res.data);
        }
      })
      .finally(() => {
        if (setToggleRender !== undefined) setToggleRender(!toggleRender);
        setLoadingAssign(false);
      });
  };

  return (
    <>
      <Table.Row
        className="bg-white dark:border-gray-700 dark:bg-gray-800"
        key={parcel.parcelNumber}
      >
        <Table.Cell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
          <span className="flex flex-row gap-1">
            <span>{parcel.parcelNumber}</span>
            <Badge
              color={
                parcel.status === "Pending"
                  ? "warning"
                  : parcel.status === "In progress"
                  ? "pink"
                  : "success"
              }
              className="text-center"
            >
              {parcel.status}
            </Badge>
          </span>
        </Table.Cell>
        {showCourier ? (
          <Table.Cell>
            {parcel.courier ? (
              <Tooltip content="View courier parcels">
                <Link
                  to={`/couriers/${parcel.courier?.id}/parcels/manage`}
                  className="hover:underline"
                >
                  {parcel.courier?.firstname + " " + parcel.courier?.lastname}
                </Link>
              </Tooltip>
            ) : (
              <>
                <Tooltip content="Click to assign">
                  <b
                    className="cursor-pointer"
                    onClick={() => setShowAssignCourierModal(true)}
                  >
                    None
                  </b>
                </Tooltip>
                <AssignCourierToParcelModal
                  show={showAssignCourierModal}
                  setShow={setShowAssignCourierModal}
                  parcel={parcel}
                  toggleRender={toggleRender}
                  setToggleRender={setToggleRender}
                  setParcel={setParcel}
                />
              </>
            )}
          </Table.Cell>
        ) : null}
        <Table.Cell>
          <span className="flex flex-col gap-2">
            <span>{parcel.senderName}</span>
            <span>{parcel.senderAddress}</span>
          </span>
        </Table.Cell>
        <Table.Cell>
          <span className="flex flex-col gap-2">
            <span>{parcel.receiverName}</span>
            <span>{parcel.receiverAddress}</span>
          </span>
        </Table.Cell>
        <Table.Cell>{parcel.weight} kg</Table.Cell>
        <Table.Cell>{parcel.price} €</Table.Cell>
        {showEditDeleteBtn ? (
          <Table.Cell>
            <span className="flex flex-wrap gap-2">
              <Button.Group outline={true}>
                <Button size="sm" onClick={editParcel}>
                  <Tooltip content="Edit Parcel">
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
                    onClick={showDeleteParcelModal}
                  >
                    <Tooltip content="Delete Parcel">
                      <BsTrash className="h-4 w-4" />
                    </Tooltip>
                  </Button>
                )}
                {loadingAssign ? (
                  <Button size="sm" color="purple">
                    <Spinner size="sm" className="h-4 w-4" light={true} />
                  </Button>
                ) : (
                  <Button
                    size="sm"
                    color="purple"
                    onClick={showUnassignParcelModal}
                    disabled={parcel.courier === null}
                  >
                    <Tooltip content="Unassign Courier">
                      <BsJournalMinus className="h-4 w-4" />
                    </Tooltip>
                  </Button>
                )}
              </Button.Group>
            </span>
          </Table.Cell>
        ) : null}
        {showDeliverBtn ? (
          <Table.Cell>
            {parcel.status === "Pending" || parcel.status === "In progress" ? (
              <Button onClick={setToDeliver}>Deliver</Button>
            ) : null}
          </Table.Cell>
        ) : null}
        {showAssignBtn ? (
          loadingAssign ? (
            <Table.Cell>
              <Button>
                <Spinner size="sm" light={true} className="mr-3" />
                <span>Assigning...</span>
              </Button>
            </Table.Cell>
          ) : (
            <Table.Cell>
              <Button onClick={assignToCourier}>Assign</Button>
            </Table.Cell>
          )
        ) : null}
      </Table.Row>

      {showEditDeleteBtn ? (
        <>
          <ConfirmModal
            show={showDeleteModal}
            setShow={setShowDeleteModal}
            onConfirm={deleteParcel}
            message={`Are you sure you want to delete ${parcel.parcelNumber} parcel?`}
            confirmText={"Yes, I'm sure"}
            cancelText={"No, cancel"}
          />
          <ConfirmModal
            show={showUnassignModal}
            setShow={setShowUnassignModal}
            onConfirm={unassignParcel}
            message={`Are you sure you want to unassign courier from ${parcel.parcelNumber} parcel?`}
            confirmText={"Yes, I'm sure"}
            cancelText={"No, cancel"}
            confirmColor="purple"
          />
          <EditParcelModal
            show={showEditModal}
            setShow={setShowEditModal}
            parcel={parcel}
            setParcel={setParcel}
          />
        </>
      ) : null}
    </>
  );
}
