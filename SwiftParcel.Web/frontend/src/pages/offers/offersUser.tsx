import { Table, Pagination, Button } from "flowbite-react";
import React from "react";
import { Header } from "../../components/header";
import { Footer } from "../../components/footer";
import { getOffers } from "../../utils/api";
import { Loader } from "../../components/loader";
import { OfferDetails } from "../../components/details/offer";

export default function Offers() {
  const [page, setPage] = React.useState(1);
  const [data, setData] = React.useState<any>(null);

  const [sortedColumn, setSortedColumn] = React.useState(null);
  const [sortDirection, setSortDirection] = React.useState('ascending');

  const [loadingHeader, setLoadingHeader] = React.useState(true);
  const [loadingOffers, setLoadingOffers] = React.useState(true);

  React.useEffect(() => {
    getOffers('7bdd78cc-0474-45bf-a00c-e8146aa09653') // to fix later
      .then((res) => {
        if (res.status === 200) {
          setData(res?.data);
        } else {
          throw new Error();
        }
      })
      .catch((err) => {
        setData(null);
      })
      .finally(() => {
        setLoadingOffers(false);
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

    const sortedData = [...data].sort((a, b) => {
      switch (column) {
        case 'inquiryId': {
            const idA = a.parcelId;
            const idB = b.parcelId;
            if (direction === 'ascending') {
                return idA > idB ? 1 : -1;
            }
            else {
                return idA < idB ? 1 : -1;
            }
        }
        case 'totalPrice': {
            const priceA = a.totalPrice;
            const priceB = b.totalPrice;
            return direction === 'ascending' ? priceA - priceB : priceB - priceA;
        }
        case 'expiringAt': {
            const dateA = new Date(a.expiringAt);
            const dateB = new Date(b.expiringAt);
            if (direction === 'ascending') {
                return dateA > dateB ? 1 : -1;
            }
            else {
                return dateA < dateB ? 1 : -1;
            }
        }
        case 'priceBreakDown': {
            const priceA = a.priceBreakDown;
            const priceB = b.priceBreakDown;
            return direction === 'ascending' ? priceA - priceB : priceB - priceA;
        }
        case 'companyName': {
            const nameA = a.companyName;
            const nameB = b.companyName;
            if (direction === 'ascending') {
                return nameA > nameB ? 1 : -1;
            }
            else {
                return nameA < nameB ? 1 : -1;
            }
        }
        default:
          return 0;
      }
    });

    setSortedColumn(column);
    setSortDirection(direction);
    setData(sortedData);
  };

  const getSortIcon = (column : string) => {
    return sortedColumn === column ? (sortDirection === 'ascending' ? '▲' : '▼') : '';
  }

  const tableHeaderStyle = {
    row: {
      display: 'flex',
      justifyContent: 'space-between',
    },
    left: {
      alignSelf: 'flex-start',
    },
    right: {
      alignSelf: 'flex-end',
    },
  };

  return (
    <>
      {loadingHeader || loadingOffers ? <Loader /> : null}
      <div className="container mx-auto px-4">
        <Header loading={loadingHeader} setLoading={setLoadingHeader} />
        <div style={tableHeaderStyle.row}>
          <h1 className="mb-2 text-3xl font-bold text-gray-900 dark:text-white" style={tableHeaderStyle.left}>
            Offers
          </h1>
        </div>
        <Table>
          <Table.Head>
            <Table.HeadCell onClick={() => handleSort('inquiryId')}>
              Inquiry's Id {getSortIcon('inquiryId')}
            </Table.HeadCell>
            <Table.HeadCell onClick={() => handleSort('totalPrice')}>
              Total price {getSortIcon('totalPrice')}
            </Table.HeadCell>
            <Table.HeadCell onClick={() => handleSort('expiringAt')}>
              Expiring date {getSortIcon('expiringAt')}
            </Table.HeadCell>
            <Table.HeadCell onClick={() => handleSort('priceBreakDown')}>
              Price break down {getSortIcon('priceBreakDown')}
            </Table.HeadCell>
            <Table.HeadCell onClick={() => handleSort('companyName')}>
              Company name {getSortIcon('companyName')}
            </Table.HeadCell>
            <Table.HeadCell>
              
            </Table.HeadCell>
          </Table.Head>
          <Table.Body className="divide-y">
            {data != null && data?.length > 0 ? (
              data?.map((offer: any) => offer != null ? (
                <OfferDetails
                  key={offer.parcelId}
                  offerData={offer}
                />
              ) : null)
            ) : (
              <tr>
                <td colSpan={6} className="text-center">
                  No offers found
                </td>
              </tr>
            )}
          </Table.Body>
        </Table>
        {data != null && data?.total_pages > 1 ? (
          <Pagination
            currentPage={page}
            onPageChange={onPageChange}
            showIcons={true}
            totalPages={data?.total_pages || 1}
          />
        ) : null}
        <Footer />
      </div>
    </>
  );
}
