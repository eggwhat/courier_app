import { Table, Pagination } from "flowbite-react";
import React from "react";
import { Header } from "../../components/header";
import { Footer } from "../../components/footer";
import { getCar, getParcelsForCar } from "../../utils/api";
import { Loader } from "../../components/loader";
import { ParcelDetails } from "../../components/details/parcel";
import { useParams } from "react-router-dom";

export default function ManageParcelsCar() {
  const [page, setPage] = React.useState(1);
  const [parcels, setParcels] = React.useState<any>(null);

  const [car, setCar] = React.useState<any>(null);

  const [loadingHeader, setLoadingHeader] = React.useState(true);
  const [loadingParcels, setLoadingParcels] = React.useState(true);

  const [toggleRender, setToggleRender] = React.useState(false);

  const { carId } = useParams();

  React.useEffect(() => {
    if (!carId) return;

    Promise.all([getParcelsForCar(Number(carId), page), getCar(Number(carId))])
      .then((res) => {
        setParcels(res[0]?.data);
        setCar(res[1]?.data);
      })
      .finally(() => {
        setLoadingParcels(false);
      });
  }, [page, carId, toggleRender]);

  const onPageChange = (page: number) => {
    setPage(page);
  };

  return (
    <>
      {loadingHeader || loadingParcels ? <Loader /> : null}
      <div className="container mx-auto px-4">
        <Header loading={loadingHeader} setLoading={setLoadingHeader} />
        {car ? (
          <>
            <h1 className="mb-2 text-3xl font-bold text-gray-900 dark:text-white">
              Car{" "}
              <span className="text-blue-700">
                {car?.make} {car?.model}
              </span>{" "}
              Parcels
            </h1>
            <Table>
              <Table.Head>
                <Table.HeadCell>#</Table.HeadCell>
                <Table.HeadCell>Courier</Table.HeadCell>
                <Table.HeadCell>Sender</Table.HeadCell>
                <Table.HeadCell>Receiver</Table.HeadCell>
                <Table.HeadCell>Weight</Table.HeadCell>
                <Table.HeadCell>Price</Table.HeadCell>
                <Table.HeadCell>
                  <span className="sr-only">Actions</span>
                </Table.HeadCell>
              </Table.Head>
              <Table.Body className="divide-y">
                {parcels?.results.length > 0 ? (
                  parcels?.results.map((parcel: any) => (
                    <ParcelDetails
                      key={parcel.parcelNumber}
                      parcelData={parcel}
                      toggleRender={toggleRender}
                      setToggleRender={setToggleRender}
                      showEditDeleteBtn={true}
                      showCourier={true}
                      showAssignBtn={undefined}
                      showDeliverBtn={undefined}
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
          <div className="flex flex-col items-center justify-center">
            <h1 className="text-3xl font-bold text-gray-900 dark:text-white">
              No Parcels
            </h1>
          </div>
        )}
        <Footer />
      </div>
    </>
  );
}
