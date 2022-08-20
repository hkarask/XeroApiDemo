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
import { useEffect, useState } from "react";
import Feedback from "../components/Feedback";
import { Account, Invoice } from "../models";

const Dashboard = () => {
  const [invoices, setInvoices] = useState<Invoice[]>([]);
  const [account, setAccount] = useState<Account>();

  useEffect(() => {
    const getData = async () => {
      const res = await fetch("/api/account");
      const account = (await res.json()) as Account;
      setAccount(account);
    };
    getData();
  }, []);

  useEffect(() => {
    const getData = async () => {
      const res = await fetch("/api/invoices");
      const invoices = (await res.json()) as Invoice[];
      setInvoices(invoices);
    };
    getData();
  }, []);

  return (
    <>
      <Box bg="bg-light" borderRadius="3xl">
        {account && <Feedback
          variant={account.isHealthy ? "success" : "warning"}
          heading={account.isHealthy ? "Account looking good" : "Account not healthy"}
          message={account.statusDescription}
        ></Feedback>}
      </Box>
      <Box bg="bg-light" borderRadius="3xl" p={4} my={4}>
        <TableContainer>
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
              {invoices.map((invoice) => (
                <Tr key={invoice.id}>
                  <Td>{invoice.dateIssued.split("T")[0]}</Td>
                  <Td>{invoice.contactName}</Td>
                  <Td isNumeric>{invoice.amount}</Td>
                  <Td isNumeric>{invoice.amountDue}</Td>
                </Tr>
              ))}
            </Tbody>
          </Table>
        </TableContainer>
      </Box>
    </>
  );
};

export default Dashboard;
