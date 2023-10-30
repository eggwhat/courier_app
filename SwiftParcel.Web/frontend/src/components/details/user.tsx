import { Table, Button, Spinner } from "flowbite-react";
import React from "react";
import { updateCourier } from "../../utils/api";

interface UserDetailsProps {
  userData: any;
  toggleRender: boolean | undefined;
  setToggleRender: ((toggleRender: boolean) => void) | undefined;
  showAssignBtn: number | undefined;
  setModalOpen: ((modelOpen: boolean) => void) | undefined;
  setCourier: ((courier: any) => void) | undefined;
}

export function UserDetails({
  userData,
  toggleRender,
  setToggleRender,
  showAssignBtn,
  setModalOpen,
  setCourier,
}: UserDetailsProps) {
  const [loadingAssign, setLoadingAssign] = React.useState(false);

  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  const [user, setUser] = React.useState<any>(userData);

  const assign = () => {
    if (!showAssignBtn) return;
    setLoadingAssign(true);

    updateCourier(showAssignBtn, { userId: user.id })
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

  return (
    <>
      <Table.Row
        className="bg-white dark:border-gray-700 dark:bg-gray-800"
        key={user.id}
      >
        <Table.Cell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
          {user.id}
        </Table.Cell>
        <Table.Cell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
          {user.username}
        </Table.Cell>
        <Table.Cell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
          {user.email}
        </Table.Cell>
        <Table.Cell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
          {user.role}
        </Table.Cell>
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
    </>
  );
}
