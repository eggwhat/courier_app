import { Table, Pagination, Button, Tooltip } from "flowbite-react";
import React from "react";
import { Header } from "../../components/header";
import { Footer } from "../../components/footer";
import { getCars } from "../../utils/api";
import { Loader } from "../../components/loader";

import { BsPlusLg } from "react-icons/bs";
import { CarDetails } from "../../components/details/car";
import { CreateCarModal } from "../../components/modals/cars/createCarModal";

export default function ManageCars() {
  const [toggleRender, setToggleRender] = React.useState(false);
  const [page, setPage] = React.useState(1);
  const [cars, setCars] = React.useState<any>(null);

  const [loadingHeader, setLoadingHeader] = React.useState(true);
  const [loadingCars, setLoadingCars] = React.useState(true);

  const [showCreateCarModal, setShowCreateCarModal] = React.useState(false);

  React.useEffect(() => {
    getCars(page)
      .then((res) => {
        setCars(res?.data);
      })
      .finally(() => {
        setLoadingCars(false);
      });
  }, [page, toggleRender]);

  const onPageChange = (page: number) => {
    setPage(page);
  };

  const createCar = () => {
    setShowCreateCarModal(true);
  };

  return (
    <>
      {loadingHeader || loadingCars ? <Loader /> : null}
      <div className="container mx-auto px-4">
        <Header loading={loadingHeader} setLoading={setLoadingHeader} />
        <div className="flex flex-wrap gap-2 mb-2">
          <h1 className="text-3xl font-bold text-gray-900 dark:text-white">
            Cars
          </h1>
          <Tooltip content="Create a new Car">
            <Button size="sm" onClick={createCar}>
              <BsPlusLg className="h-4 w-4" />
            </Button>
          </Tooltip>
        </div>

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
                toggleRender={toggleRender}
                setToggleRender={setToggleRender}
                showEditDeleteBtn={true}
                showAssignBtn={undefined}
                setModalOpen={undefined}
                setCourier={undefined}
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
        <CreateCarModal
          show={showCreateCarModal}
          setShow={setShowCreateCarModal}
          toggleRender={toggleRender}
          setToggleRender={setToggleRender}
        />
        <Footer />
      </div>
    </>
  );
}
