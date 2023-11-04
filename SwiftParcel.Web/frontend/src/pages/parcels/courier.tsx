import { Table, Pagination, Button, Tooltip } from "flowbite-react";
import React from "react";
import { Header } from "../../components/header";
import { Footer } from "../../components/footer";
import { getCourier, getParcelsForCourier } from "../../utils/api";
import { Loader } from "../../components/loader";

import { MdAssignment } from "react-icons/md";
import { ParcelDetails } from "../../components/details/parcel";
import { useParams } from "react-router-dom";
import { AssignParcelModal } from "../../components/modals/assignParcelModal";

export default function ManageParcelsCourier() {
  const [toggleRender, setToggleRender] = React.useState(false);
  const [page, setPage] = React.useState(1);
  const [parcels, setParcels] = React.useState<any>(null);
  const [courier, setCourier] = React.useState<any>(null);

  const [loadingHeader, setLoadingHeader] = React.useState(true);
  const [loadingParcels, setLoadingParcels] = React.useState(true);

  const [showAssignParcelModal, setShowAssignParcelModal] =
    React.useState(false);

  const { courierId } = useParams();

  React.useEffect(() => {
    if (!courierId) return;

    Promise.all([
      getParcelsForCourier(Number(courierId), page),
      getCourier(Number(courierId)),
    ])
      .then((res) => {
        setParcels(res[0]?.data);
        setCourier(res[1]?.data);
      })
      .finally(() => {
        setLoadingParcels(false);
      });
  }, [courierId, page, toggleRender]);

  const onPageChange = (page: number) => {
    setPage(page);
  };

  const assignParcel = () => {
    setShowAssignParcelModal(true);
  };

  return (
    <>
      {loadingHeader || loadingParcels ? <Loader /> : null}
      <div className="container mx-auto px-4">
        <Header loading={loadingHeader} setLoading={setLoadingHeader} />
        {courier ? (
          <>
            <div className="flex flex-wrap gap-2 mb-2">
              <h1 className="text-3xl font-bold text-gray-900 dark:text-white">
                Courier{" "}
                <span className="text-blue-700">
                  {courier?.firstname} {courier?.lastname}
                </span>{" "}
                Parcels
              </h1>
              <Tooltip content="Assign a Parcel">
                <Button size="sm" onClick={assignParcel}>
                  <MdAssignment className="w-5 h-5" />
                </Button>
              </Tooltip>
            </div>

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
                {parcels != null && parcels?.results?.length > 0 ? (
                  parcels?.results.map((parcel: any) => (
                    <ParcelDetails
                      key={parcel.parcelNumber}
                      parcelData={parcel}
                      toggleRender={toggleRender}
                      setToggleRender={setToggleRender}
                      showEditDeleteBtn={true}
                      showCourier={undefined}
                      showDeliverBtn={undefined}
                      showAssignBtn={undefined}
                    />
                  ))
                ) : (
                  <tr>
                    <td colSpan={6} className="text-center">
                      No parcels found
                    </td>
                  </tr>
                )}
              </Table.Body>
            </Table>
            {parcels != null && parcels?.total_pages > 1 ? (
              <Pagination
                currentPage={page}
                onPageChange={onPageChange}
                showIcons={true}
                totalPages={parcels?.total_pages || 1}
              />
            ) : null}
          </>
        ) : (
          <div className="flex flex-col items-center justify-center">
            <h1 className="text-3xl font-bold text-gray-900 dark:text-white">
              No Parcels
            </h1>
          </div>
        )}
        <AssignParcelModal
          show={showAssignParcelModal}
          setShow={setShowAssignParcelModal}
          courier={courier}
          setCourier={setCourier}
          toggleRender={toggleRender}
          setToggleRender={setToggleRender}
        />
        <Footer />
      </div>
    </>
  );
}
