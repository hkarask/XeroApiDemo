import {
  Box,
  Table,
  TableCaption,
  TableContainer,
  Tbody,
  Td,
  Th,
  Thead,
  Tr,
} from "@chakra-ui/react";
import Feedback from "../components/Feedback";
import { Account, Invoice } from "../models";
import useRequest from "../hooks/useRequest";
import Error from "../components/Error";
import Skeleton from "../components/Skeleton";

const Dashboard = () => {
  var accountReq = useRequest<Account>("/api/account");
  var invoicesReq = useRequest<Invoice[]>("/api/invoices");

  return (
    <>
      <Box bg="bg-light" borderRadius="3xl" p={4} my={4}>
        <Skeleton isLoaded={!accountReq.loading} />
        <Error title="Account" description={accountReq.error} />
        {accountReq.data && (
          <Feedback
            variant={accountReq.data.isHealthy ? "success" : "warning"}
            heading={
              accountReq.data.isHealthy
                ? "Account looking good"
                : "Account not healthy"
            }
            message={accountReq.data.statusDescription}
          ></Feedback>
        )}
      </Box>
      <Box bg="bg-light" borderRadius="3xl" p={4} my={4}>
        <Skeleton isLoaded={!invoicesReq.loading} />
        <Error title="Invoices" description={invoicesReq.error} />
        <TableContainer>
          {invoicesReq.data && (
            <Table variant="striped">
              <TableCaption placement="top">Latest invoices</TableCaption>
              <Thead>
                <Tr>
                  <Th>Date Issued</Th>
                  <Th>Contact</Th>
                  <Th isNumeric>Amount</Th>
                  <Th isNumeric>amountDue</Th>
                </Tr>
              </Thead>
              <Tbody>
                {invoicesReq.data?.map((invoice) => (
                  <Tr key={invoice.id}>
                    <Td>{invoice.dateIssued.split("T")[0]}</Td>
                    <Td>{invoice.contactName}</Td>
                    <Td isNumeric>{invoice.amount}</Td>
                    <Td isNumeric>{invoice.amountDue}</Td>
                  </Tr>
                ))}
              </Tbody>
            </Table>
          )}
        </TableContainer>
      </Box>
    </>
  );
};

export default Dashboard;
