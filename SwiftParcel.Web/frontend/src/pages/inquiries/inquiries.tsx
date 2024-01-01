import { Table, Pagination } from "flowbite-react";
import React from "react";
import { Header } from "../../components/header";
import { Footer } from "../../components/footer";
import { getInquiries } from "../../utils/api";
import { Loader } from "../../components/loader";
import { InquiryDetails } from "../../components/details/inquiry";

export default function Inquiries() {
  const [page, setPage] = React.useState(1);
  const [inquiries, setInquiries] = React.useState<any>(null);

  const [loadingHeader, setLoadingHeader] = React.useState(true);
  const [loadingInquiries, setLoadingInquiries] = React.useState(true);

  React.useEffect(() => {
    getInquiries()
      .then((res) => {
        if (res.status === 200) {
          setInquiries(res?.data);
        } else {
          throw new Error();
        }
      })
      .catch((err) => {
        setInquiries(null);
      })
      .finally(() => {
        setLoadingInquiries(false);
      });
  }, [page]);

  const onPageChange = (page: number) => {
    setPage(page);
  };

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
            <Table.HeadCell>Id</Table.HeadCell>
            <Table.HeadCell>Package dimensions</Table.HeadCell>
            <Table.HeadCell>Package weight</Table.HeadCell>
            <Table.HeadCell>Source address</Table.HeadCell>
            <Table.HeadCell>Destination address</Table.HeadCell>
            <Table.HeadCell>Date of inquiring</Table.HeadCell>
            <Table.HeadCell>Status</Table.HeadCell>
          </Table.Head>
          <Table.Body className="divide-y">
            {inquiries != null && inquiries?.length > 0 ? (
              inquiries?.map((inquiry: any) => (
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
        {inquiries != null && inquiries?.total_pages > 1 ? (
          <Pagination
            currentPage={page}
            onPageChange={onPageChange}
            showIcons={true}
            totalPages={inquiries?.total_pages || 1}
          />
        ) : null}
        <Footer />
      </div>
    </>
  );
}
