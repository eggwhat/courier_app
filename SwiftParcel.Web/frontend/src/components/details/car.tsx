import { Table, Button, Tooltip, Spinner } from "flowbite-react";
import React from "react";
import { BsPencil, BsTrash } from "react-icons/bs";
import { AiOutlineEye } from "react-icons/ai";
import { ConfirmModal } from "../modals/confirmModal";
import { deleteCar, updateCourier } from "../../utils/api";
import { EditCarModal } from "../modals/cars/editCarModal";
import { useNavigate } from "react-router-dom";

interface CarDetailsProps {
  carData: any;
  toggleRender: boolean | undefined;
  setToggleRender: ((toggleRender: boolean) => void) | undefined;
  showEditDeleteBtn: boolean | undefined;
  showAssignBtn: number | undefined;
  setModalOpen: ((modelOpen: boolean) => void) | undefined;
  setCourier: ((courier: any) => void) | undefined;
}

export function CarDetails({
  carData,
  toggleRender,
  setToggleRender,
  showEditDeleteBtn,
  showAssignBtn,
  setModalOpen,
  setCourier,
}: CarDetailsProps) {
  const [showDeleteModal, setShowDeleteModal] = React.useState(false);
  const [showEditModal, setShowEditModal] = React.useState(false);

  const [loadingDelete, setLoadingDelete] = React.useState(false);
  const [loadingAssign, setLoadingAssign] = React.useState(false);

  const [car, setCar] = React.useState<any>(carData);

  const navigate = useNavigate();

  const edit = () => {
    if (!showEditDeleteBtn) return;
    setShowEditModal(true);
  };

  const remove = () => {
    if (!showEditDeleteBtn) return;
    setShowDeleteModal(false);
    setLoadingDelete(true);
    deleteCar(car.id)
      .then((res) => {
        if (res?.status === 204) {
          if (setToggleRender !== undefined) setToggleRender(!toggleRender);
        }
      })
      .finally(() => {
        setLoadingDelete(false);
      });
  };

  const assign = () => {
    if (!showAssignBtn) return;
    setLoadingAssign(true);

    updateCourier(showAssignBtn, { carId: car.id })
      .then((res) => {
        if (res?.status === 200) {
          if (setCourier !== undefined) setCourier(res.data);
          if (setToggleRender !== undefined) setToggleRender(!toggleRender);
          if (setModalOpen !== undefined) setModalOpen(false);
        }
      })
      .finally(() => {
        setLoadingAssign(false);
      });
  };

  const showRemoveModal = () => {
    if (!showEditDeleteBtn) return;
    setShowDeleteModal(true);
  };

  return (
    <>
      <Table.Row
        className="bg-white dark:border-gray-700 dark:bg-gray-800"
        key={car.id}
      >
        <Table.Cell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
          {car.id}
        </Table.Cell>
        <Table.Cell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
          {car.make}
        </Table.Cell>
        <Table.Cell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
          {car.model}
        </Table.Cell>
        <Table.Cell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
          {car.licensePlate}
        </Table.Cell>
        {showEditDeleteBtn ? (
          <Table.Cell>
            <span className="flex flex-wrap gap-2">
              <Button.Group outline={true}>
                <Button size="sm" onClick={edit}>
                  <Tooltip content="Edit Car">
                    <BsPencil className="h-4 w-4" />
                  </Tooltip>
                </Button>
                {loadingDelete ? (
                  <Button size="sm" color="failure">
                    <Spinner size="sm" className="h-4 w-4" light={true} />
                  </Button>
                ) : (
                  <Button size="sm" color="failure" onClick={showRemoveModal}>
                    <Tooltip content="Delete Car">
                      <BsTrash className="h-4 w-4" />
                    </Tooltip>
                  </Button>
                )}
                <Button
                  size="sm"
                  color="purple"
                  onClick={() => navigate(`/cars/${car.id}/parcels/manage`)}
                >
                  <Tooltip content="View Parcels">
                    <AiOutlineEye className="h-4 w-4" />
                  </Tooltip>
                </Button>
              </Button.Group>
            </span>
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
              <Button onClick={assign}>Assign</Button>
            </Table.Cell>
          )
        ) : null}
      </Table.Row>

      {showEditDeleteBtn ? (
        <>
          <ConfirmModal
            show={showDeleteModal}
            setShow={setShowDeleteModal}
            onConfirm={remove}
            message={`Are you sure you want to delete car ${car.make} ${car.model}?`}
            confirmText={"Yes, I'm sure"}
            cancelText={"No, cancel"}
          />
          <EditCarModal
            show={showEditModal}
            setShow={setShowEditModal}
            car={car}
            setCar={setCar}
          />
        </>
      ) : null}
    </>
  );
}
