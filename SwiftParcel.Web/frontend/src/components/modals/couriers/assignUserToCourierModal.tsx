import { Modal, Pagination, Table } from "flowbite-react";
import React from "react";
import { getUsers } from "../../../utils/api";
import { UserDetails } from "../../details/user";

interface AssignUserToCourierModalProps {
  show: boolean;
  setShow: (show: boolean) => void;
  courier: any;
  setCourier: (courier: any) => void;
  toggleRender: boolean | undefined;
  setToggleRender: ((toggleRender: boolean) => void) | undefined;
}

export function AssignUserToCourierModal(props: AssignUserToCourierModalProps) {
  const close = () => {
    props.setShow(false);
  };

  const [page, setPage] = React.useState(1);
  const [users, setUsers] = React.useState<any>(null);

  const [isLoading, setIsLoading] = React.useState(true);

  React.useMemo(() => {
    if (props.show === false) return;
    setIsLoading(true);
    getUsers(page, 5, "&unassigned=true")
      .then((res) => {
        setUsers(res?.data);
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
              Assign User to Courier{" "}
              <span className="text-blue-700">
                {props.courier?.firstname} {props.courier?.lastname}
              </span>
            </h3>
            {isLoading ? (
              <div className="flex justify-center">
                <div className="animate-spin rounded-full h-32 w-32 border-b-2 border-blue-700"></div>
              </div>
            ) : users?.results?.length > 0 ? (
              <>
                <Table>
                  <Table.Head>
                    <Table.HeadCell>#</Table.HeadCell>
                    <Table.HeadCell>Username</Table.HeadCell>
                    <Table.HeadCell>Email</Table.HeadCell>
                    <Table.HeadCell>Role</Table.HeadCell>
                    <Table.HeadCell>
                      <span className="sr-only">Actions</span>
                    </Table.HeadCell>
                  </Table.Head>
                  <Table.Body className="divide-y">
                    {users?.results.map((user: any) => (
                      <UserDetails
                        key={user.id}
                        userData={user}
                        toggleRender={props.toggleRender}
                        setToggleRender={props.setToggleRender}
                        showAssignBtn={props.courier?.id}
                        setModalOpen={props.setShow}
                        setCourier={props.setCourier}
                      />
                    ))}
                  </Table.Body>
                </Table>
                {users?.total_pages > 1 ? (
                  <Pagination
                    currentPage={page}
                    onPageChange={onPageChange}
                    showIcons={true}
                    totalPages={users?.total_pages || 1}
                  />
                ) : null}
              </>
            ) : (
              <div className="text-center">
                <h3 className="text-lg font-medium text-gray-900 dark:text-white">
                  No users to assign
                </h3>
              </div>
            )}
          </div>
        </Modal.Body>
      </Modal>
    </React.Fragment>
  );
}
