import { Modal, Pagination, Table } from "flowbite-react";
import React from "react";
import { Link } from "react-router-dom";
import { getCouriers } from "../../../utils/api";
import { CourierDetails } from "../../details/courier";

interface AssignCourierToParcelModalProps {
  show: boolean;
  setShow: (show: boolean) => void;
  parcel: any;
  setParcel: (parcel: any) => void;
  toggleRender: boolean | undefined;
  setToggleRender: ((toggleRender: boolean) => void) | undefined;
}

export function AssignCourierToParcelModal(
  props: AssignCourierToParcelModalProps
) {
  const close = () => {
    props.setShow(false);
  };

  const [page, setPage] = React.useState(1);
  const [couriers, setCouriers] = React.useState<any>(null);

  const [isLoading, setIsLoading] = React.useState(true);

  React.useMemo(() => {
    if (props.show === false) return;
    setIsLoading(true);
    getCouriers(page, 5)
      .then((res) => {
        setCouriers(res?.data);
      })
      .finally(() => {
        setIsLoading(false);
      });
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [page, props.toggleRender, props.show]);

  const onPageChange = (page: number) => {
    setPage(page);
  };

  return (
    <React.Fragment>
      <Modal show={props.show} size="7xl" popup={true} onClose={close}>
        <Modal.Header />
        <Modal.Body>
          <div className="space-y-6 px-6 pb-4 sm:pb-6 lg:px-8 xl:pb-8">
            <h3 className="text-xl font-medium text-gray-900 dark:text-white">
              Assign Courier to Parcel{" "}
              <span className="text-blue-700">
                {props.parcel?.parcelNumber}
              </span>
            </h3>
            {isLoading ? (
              <div className="flex justify-center">
                <div className="animate-spin rounded-full h-32 w-32 border-b-2 border-blue-700"></div>
              </div>
            ) : couriers?.results?.length > 0 ? (
              <>
                <Table>
                  <Table.Head>
                    <Table.HeadCell>Firstname</Table.HeadCell>
                    <Table.HeadCell>Lastname</Table.HeadCell>
                    <Table.HeadCell>Phone</Table.HeadCell>
                    <Table.HeadCell>
                      <span className="sr-only">Actions</span>
                    </Table.HeadCell>
                  </Table.Head>
                  <Table.Body className="divide-y">
                    {couriers?.results.map((courier: any) => (
                      <CourierDetails
                        key={courier.id}
                        courierData={courier}
                        setParcel={props.setParcel}
                        toggleRender={props.toggleRender}
                        setToggleRender={props.setToggleRender}
                        showAssignParcelBtn={props.parcel?.parcelNumber}
                        setModalOpen={props.setShow}
                        showEditDeleteBtn={undefined}
                        showUser={undefined}
                        showCar={undefined}
                      />
                    ))}
                  </Table.Body>
                </Table>
                {couriers?.total_pages > 1 ? (
                  <Pagination
                    currentPage={page}
                    onPageChange={onPageChange}
                    showIcons={true}
                    totalPages={couriers?.total_pages || 1}
                  />
                ) : null}
              </>
            ) : (
              <div className="text-center">
                <h3 className="text-lg font-medium text-gray-900 dark:text-white">
                  No couriers to assign, please{" "}
                  <Link to="/couriers/manage" className="hover:underline">
                    Add a new one
                  </Link>
                </h3>
              </div>
            )}
          </div>
        </Modal.Body>
      </Modal>
    </React.Fragment>
  );
}
