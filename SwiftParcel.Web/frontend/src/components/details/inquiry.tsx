import { Table, Badge, Button, Tooltip, Spinner } from "flowbite-react";
import React from "react";
import { BsJournalMinus, BsPencil, BsTrash } from "react-icons/bs";
import { Link, useNavigate } from "react-router-dom";
import { ConfirmModal } from "../modals/confirmModal";
import { EditParcelModal } from "../modals/parcels/editParcelModal";
import { AssignCourierToParcelModal } from "../modals/parcels/assignCourierToParcelModal";

interface InquiryDetailsProps {
  inquiryData: any;
  toggleRender: boolean | undefined;
  setToggleRender: ((toggleRender: boolean) => void) | undefined;
  showEditDeleteBtn: boolean | undefined;
  showDeliverBtn: boolean | undefined;
  showAssignBtn: number | undefined;
}

export function InquiryDetails({
  inquiryData,
  showEditDeleteBtn,
  showDeliverBtn,
  showAssignBtn,
}: InquiryDetailsProps) {
  const [loadingDelete, setLoadingDelete] = React.useState(false);
  const [loadingAssign, setLoadingAssign] = React.useState(false);

  const [inquiry, setInquiry] = React.useState<any>(inquiryData);

  const navigate = useNavigate();

  return (
    <>
      <Table.Row
        className="bg-white dark:border-gray-700 dark:bg-gray-800"
        key={inquiry.parcelNumber}
      >
        <Table.Cell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
          <span className="flex flex-row gap-1">
            <span>{inquiry.parcelNumber}</span>
            <Badge
              color={
                inquiry.status === "Pending"
                  ? "warning"
                  : inquiry.status === "In progress"
                  ? "pink"
                  : "success"
              }
              className="text-center"
            >
              {inquiry.status}
            </Badge>
          </span>
        </Table.Cell>
        <Table.Cell>
          <span className="flex flex-col gap-2">
            <span>{inquiry.sourceCity}</span>
            <span>{inquiry.sourceCountry}</span>
          </span>
        </Table.Cell>
        <Table.Cell>
          <span className="flex flex-col gap-2">
            <span>{inquiry.destinationCity}</span>
            <span>{inquiry.destinationCountry}</span>
          </span>
        </Table.Cell>
        <Table.Cell>{inquiry.width} cm x {inquiry.height} x {inquiry.depth}</Table.Cell>
        <Table.Cell>{inquiry.weight} kg</Table.Cell>
        <Table.Cell>{inquiry.price} PLN</Table.Cell>
        {showEditDeleteBtn ? (
          <Table.Cell>
            <span className="flex flex-wrap gap-2">
              <Button.Group outline={true}>
                <Button size="sm">
                  <Tooltip content="Edit Parcel">
                    <BsPencil className="h-4 w-4" />
                  </Tooltip>
                </Button>
                {loadingDelete ? (
                  <Button size="sm" color="failure">
                    <Spinner size="sm" className="h-4 w-4" light={true} />
                  </Button>
                ) : (
                  <Button
                    size="sm"
                    color="failure"
                  >
                    <Tooltip content="Delete Parcel">
                      <BsTrash className="h-4 w-4" />
                    </Tooltip>
                  </Button>
                )}
                {loadingAssign ? (
                  <Button size="sm" color="purple">
                    <Spinner size="sm" className="h-4 w-4" light={true} />
                  </Button>
                ) : (
                  <Button
                    size="sm"
                    color="purple"
                    disabled={inquiry.courier === null}
                  >
                    <Tooltip content="Unassign Courier">
                      <BsJournalMinus className="h-4 w-4" />
                    </Tooltip>
                  </Button>
                )}
              </Button.Group>
            </span>
          </Table.Cell>
        ) : null}
        {showDeliverBtn ? (
          <Table.Cell>
            {inquiry.status === "Pending" || inquiry.status === "In progress" ? (
              <Button>Deliver</Button>
            ) : null}
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
              <Button>Assign</Button>
            </Table.Cell>
          )
        ) : null}
      </Table.Row>
    </>
  );
}
