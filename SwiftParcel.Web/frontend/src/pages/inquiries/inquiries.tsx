import { Table, Pagination } from "flowbite-react";
import React from "react";
import { Header } from "../../components/header";
import { Footer } from "../../components/footer";
import { getInquiries } from "../../utils/api";
import { Loader } from "../../components/loader";
import { InquiryDetails } from "../../components/details/inquiry";
import { isPackageValid } from "../../components/details/inquiry";

export default function Inquiries() {
  const [page, setPage] = React.useState(1);
  const [tableData, setTableData] = React.useState<any>(null);

  const [sortedColumn, setSortedColumn] = React.useState(null);
  const [sortDirection, setSortDirection] = React.useState('ascending');

  const [loadingHeader, setLoadingHeader] = React.useState(true);
  const [loadingInquiries, setLoadingInquiries] = React.useState(true);

  React.useEffect(() => {
    getInquiries()
      .then((res) => {
        if (res.status === 200) {
          setTableData(res?.data);
        } else {
          throw new Error();
        }
      })
      .catch((err) => {
        setTableData(null);
      })
      .finally(() => {
        setLoadingInquiries(false);
      });
  }, [page]);

  const onPageChange = (page: number) => {
    setPage(page);
  };

  const handleSort = (column : string) => {
    let direction = 'ascending';
    if (sortedColumn === column && sortDirection === 'ascending') {
      direction = 'descending';
    }

    const sortedData = [...tableData].sort((a, b) => {
      switch (column) {
        case 'id': {
          const idA = a.id;
          const idB = b.id;
          if (direction === 'ascending') {
            return idA > idB ? 1 : -1;
          }
          else {
            return idA < idB ? 1 : -1;
          }
        }
        case 'packageDimensions': {
          const volumeA = a.width * a.height * a.depth;
          const volumeB = b.width * b.height * b.depth;
          return direction === 'ascending' ? volumeA - volumeB : volumeB - volumeA;
        }
        case 'packageWeight': {
          const weightA = a.weight;
          const weightB = b.weight;
          return direction === 'ascending' ? weightA - weightB : weightB - weightA;
        }
        case 'sourceAddress': {
          const addressA = `${a.source.street} ${a.source.buildingNumber} ${a.source.apartmentNumber}
            ${a.source.zipCode} ${a.source.city} ${a.source.country}`;
          const addressB = `${b.source.street} ${b.source.buildingNumber} ${b.source.apartmentNumber}
            ${b.source.zipCode} ${b.source.city} ${b.source.country}`;
          if (direction === 'ascending') {
            return addressA > addressB ? 1 : -1;
          }
          else {
            return addressA < addressB ? 1 : -1;
          }
        }
        case 'destinationAddress': {
          const addressA = `${a.destination.street} ${a.destination.buildingNumber} ${a.destination.apartmentNumber}
            ${a.destination.zipCode} ${a.destination.city} ${a.destination.country}`;
          const addressB = `${b.destination.street} ${b.destination.buildingNumber} ${b.destination.apartmentNumber}
            ${b.destination.zipCode} ${b.destination.city} ${b.destination.country}`;
          if (direction === 'ascending') {
            return addressA > addressB ? 1 : -1;
          }
          else {
            return addressA < addressB ? 1 : -1;
          }
        }
        case 'dateOfInquiring': {
          const dateA = new Date(a.createdAt);
          const dateB = new Date(b.createdAt);
          if (direction === 'ascending') {
            return dateA > dateB ? 1 : -1;
          }
          else {
            return dateA < dateB ? 1 : -1;
          }
        }
        case 'status': {
          const statusA = isPackageValid(a.validTo) ? "valid" : "expired";
          const statusB = isPackageValid(b.validTo) ? "valid" : "expired";
          if (direction === 'ascending') {
            return statusA > statusB ? 1 : -1;
          }
          else {
            return statusA < statusB ? 1 : -1;
          }
        }
        default:
          return 0;
      }
    });

    setSortedColumn(column);
    setSortDirection(direction);
    setTableData(sortedData);
  };

  const getSortIcon = (column : string) => {
    return sortedColumn === column ? (sortDirection === 'ascending' ? '▲' : '▼') : '';
  }

  return (
    <>
      {loadingHeader || loadingInquiries ? <Loader /> : null}
      <div className="container mx-auto px-4">
        <Header loading={loadingHeader} setLoading={setLoadingHeader} />
        <h1 className="mb-2 text-3xl font-bold text-gray-900 dark:text-white">
          Inquiries
        </h1>
        <Table>
          <Table.Head>
            <Table.HeadCell onClick={() => handleSort('id')}>
              Id {getSortIcon('id')}
            </Table.HeadCell>
            <Table.HeadCell onClick={() => handleSort('packageDimensions')}>
              Package dimensions {getSortIcon('packageDimensions')}
            </Table.HeadCell>
            <Table.HeadCell onClick={() => handleSort('packageWeight')}>
              Package weight {getSortIcon('packageWeight')}
            </Table.HeadCell>
            <Table.HeadCell onClick={() => handleSort('sourceAddress')}>
              Source address {getSortIcon('sourceAddress')}
            </Table.HeadCell>
            <Table.HeadCell onClick={() => handleSort('destinationAddress')}>
              Destination address {getSortIcon('destinationAddress')}
            </Table.HeadCell>
            <Table.HeadCell onClick={() => handleSort('dateOfInquiring')}>
              Date of inquiring {getSortIcon('dateOfInquiring')}
            </Table.HeadCell>
            <Table.HeadCell onClick={() => handleSort('status')}>
              Status {getSortIcon('status')}
            </Table.HeadCell>
          </Table.Head>
          <Table.Body className="divide-y">
            {tableData != null && tableData?.length > 0 ? (
              tableData?.map((inquiry: any) => (
                <InquiryDetails
                  key={inquiry.id}
                  inquiryData={inquiry}
                  showDeliverBtn={true}
                  toggleRender={false}
                  setToggleRender={function (toggleRender: boolean): void {
                    throw new Error("Function not implemented.");
                  }}
                  showEditDeleteBtn={undefined}
                  showAssignBtn={undefined}
                />
              ))
            ) : (
              <tr>
                <td colSpan={6} className="text-center">
                  No inquiries found
                </td>
              </tr>
            )}
          </Table.Body>
        </Table>
        {tableData != null && tableData?.total_pages > 1 ? (
          <Pagination
            currentPage={page}
            onPageChange={onPageChange}
            showIcons={true}
            totalPages={tableData?.total_pages || 1}
          />
        ) : null}
        <Footer />
      </div>
    </>
  );
}
