import { Modal, Pagination, Table } from "flowbite-react";
import React from "react";
import { Link } from "react-router-dom";
import { getCars } from "../../../utils/api";
import { CarDetails } from "../../details/car";

interface AssignCarToCourierModalProps {
  show: boolean;
  setShow: (show: boolean) => void;
  courier: any;
  setCourier: (courier: any) => void;
  toggleRender: boolean | undefined;
  setToggleRender: ((toggleRender: boolean) => void) | undefined;
}

export function AssignCarToCourierModal(props: AssignCarToCourierModalProps) {
  const close = () => {
    props.setShow(false);
  };

  const [page, setPage] = React.useState(1);
  const [cars, setCars] = React.useState<any>(null);

  const [isLoading, setIsLoading] = React.useState(true);

  React.useMemo(() => {
    if (props.show === false) return;
    setIsLoading(true);
    getCars(page, 5)
      .then((res) => {
        setCars(res?.data);
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
              Assign Car to Courier{" "}
              <span className="text-blue-700">
                {props.courier?.firstname} {props.courier?.lastname}
              </span>
            </h3>
            {isLoading ? (
              <div className="flex justify-center">
                <div className="animate-spin rounded-full h-32 w-32 border-b-2 border-blue-700"></div>
              </div>
            ) : cars?.results?.length > 0 ? (
              <>
                <Table>
                  <Table.Head>
                    <Table.HeadCell>#</Table.HeadCell>
                    <Table.HeadCell>Make</Table.HeadCell>
                    <Table.HeadCell>Model</Table.HeadCell>
                    <Table.HeadCell>License Plate</Table.HeadCell>
                    <Table.HeadCell>
                      <span className="sr-only">Actions</span>
                    </Table.HeadCell>
                  </Table.Head>
                  <Table.Body className="divide-y">
                    {cars?.results.map((car: any) => (
                      <CarDetails
                        key={car.id}
                        carData={car}
                        toggleRender={props.toggleRender}
                        setToggleRender={props.setToggleRender}
                        showEditDeleteBtn={undefined}
                        showAssignBtn={props.courier?.id}
                        setModalOpen={props.setShow}
                        setCourier={props.setCourier}
                      />
                    ))}
                  </Table.Body>
                </Table>
                {cars?.total_pages > 1 ? (
                  <Pagination
                    currentPage={page}
                    onPageChange={onPageChange}
                    showIcons={true}
                    totalPages={cars?.total_pages || 1}
                  />
                ) : null}
              </>
            ) : (
              <div className="text-center">
                <h3 className="text-lg font-medium text-gray-900 dark:text-white">
                  No cars to assign, please{" "}
                  <Link to="/cars/manage" className="hover:underline">
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
