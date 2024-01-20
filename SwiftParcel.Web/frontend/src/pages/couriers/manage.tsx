import { Table, Pagination, Button, Tooltip } from "flowbite-react";
import React from "react";
import { Header } from "../../components/header";
import { Footer } from "../../components/footer";
import { getCouriers } from "../../utils/api";
import { Loader } from "../../components/loader";

import { BsPlusLg } from "react-icons/bs";
import { CourierDetails } from "../../components/details/courier";
import { CreateCourierModal } from "../../components/modals/couriers/createCourierModal";

export default function ManageCouriers() {
  const [toggleRender, setToggleRender] = React.useState(false);
  const [page, setPage] = React.useState(1);
  const [couriers, setCouriers] = React.useState<any>(null);

  const [loadingHeader, setLoadingHeader] = React.useState(true);
  const [loadingCouriers, setLoadingCouriers] = React.useState(true);

  const [showCreateCourierModal, setShowCreateCourierModal] =
    React.useState(false);

  React.useEffect(() => {
    getCouriers(page)
      .then((res) => {
        setCouriers(res?.data);
      })
      .finally(() => {
        setLoadingCouriers(false);
      });
  }, [page, toggleRender]);

  const onPageChange = (page: number) => {
    setPage(page);
  };

  const createCourier = () => {
    setShowCreateCourierModal(true);
  };

  return (
    <>
      {loadingHeader || loadingCouriers ? <Loader /> : null}
      <div className="container mx-auto px-4">
        <Header loading={loadingHeader} setLoading={setLoadingHeader} />
        <div className="flex flex-wrap gap-2 mb-2">
          <h1 className="text-3xl font-bold text-gray-900 dark:text-white">
            Couriers
          </h1>
          <Tooltip content="Create a new Courier">
            <Button size="sm" onClick={createCourier}>
              <BsPlusLg className="h-4 w-4" />
            </Button>
          </Tooltip>
        </div>

        <Table>
          <Table.Head>
            <Table.HeadCell>Firstname</Table.HeadCell>
            <Table.HeadCell>Lastname</Table.HeadCell>
            <Table.HeadCell>Phone</Table.HeadCell>
            <Table.HeadCell>User</Table.HeadCell>
            <Table.HeadCell>Car</Table.HeadCell>
            <Table.HeadCell>
              <span className="sr-only">Actions</span>
            </Table.HeadCell>
          </Table.Head>
          <Table.Body className="divide-y">
            {couriers?.results.map((courier: any) => (
              <CourierDetails
                key={courier.id}
                courierData={courier}
                toggleRender={toggleRender}
                setToggleRender={setToggleRender}
                showUser={true}
                showCar={true}
                showEditDeleteBtn={true}
                showAssignParcelBtn={undefined}
                setModalOpen={undefined}
                setParcel={undefined}
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
        <CreateCourierModal
          show={showCreateCourierModal}
          setShow={setShowCreateCourierModal}
          toggleRender={toggleRender}
          setToggleRender={setToggleRender}
        />
        <Footer />
      </div>
    </>
  );
}
