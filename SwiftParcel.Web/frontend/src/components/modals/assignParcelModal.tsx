import { Modal, Pagination, Table } from "flowbite-react";
import React from "react";
import { Link } from "react-router-dom";
import { getParcels } from "../../utils/api";
import { ParcelDetails } from "../details/parcel";

interface AssignParcelModalProps {
  show: boolean;
  setShow: (show: boolean) => void;
  courier: any;
  setCourier: (parcel: any) => void;
  toggleRender: boolean;
  setToggleRender: (toggleRender: boolean) => void;
}

export function AssignParcelModal(props: AssignParcelModalProps) {
  const close = () => {
    props.setShow(false);
  };

  const [page, setPage] = React.useState(1);
  const [parcels, setParcels] = React.useState<any>(null);

  React.useEffect(() => {
    getParcels(page, 5, "&unassigned=true").then((res) => {
      setParcels(res?.data);
    });
  }, [page, props.toggleRender]);

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
              Assign Parcel to Courier{" "}
              <span className="text-blue-700">
                {props.courier?.firstname} {props.courier?.lastname}
              </span>
            </h3>
            {parcels?.results?.length > 0 ? (
              <>
                <Table>
                  <Table.Head>
                    <Table.HeadCell>#</Table.HeadCell>
                    <Table.HeadCell>Sender</Table.HeadCell>
                    <Table.HeadCell>Receiver</Table.HeadCell>
                    <Table.HeadCell>Weight</Table.HeadCell>
                    <Table.HeadCell>Price</Table.HeadCell>
                    <Table.HeadCell>
                      <span className="sr-only">Actions</span>
                    </Table.HeadCell>
                  </Table.Head>
                  <Table.Body className="divide-y">
                    {parcels?.results.map((parcel: any) => (
                      <ParcelDetails
                        key={parcel.parcelNumber}
                        parcelData={parcel}
                        showAssignBtn={props.courier?.id}
                        toggleRender={props.toggleRender}
                        setToggleRender={props.setToggleRender}
                        showEditDeleteBtn={undefined}
                        showCourier={undefined}
                        showDeliverBtn={undefined}
                      />
                    ))}
                  </Table.Body>
                </Table>
                {parcels?.total_pages > 1 ? (
                  <Pagination
                    currentPage={page}
                    onPageChange={onPageChange}
                    showIcons={true}
                    totalPages={parcels?.total_pages || 1}
                  />
                ) : null}
              </>
            ) : (
              <div className="text-center">
                <h3 className="text-lg font-medium text-gray-900 dark:text-white">
                  No parcels to assign, please{" "}
                  <Link to="/parcels/manage" className="hover:underline">
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
