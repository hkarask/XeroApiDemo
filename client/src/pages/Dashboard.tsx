import { Box } from "@chakra-ui/react";
import { useState } from "react";

const Dashboard = () => {
  return (
    <>
      {[...Array(15).keys()].map((x) => (
        <Box key={x} bg="bg-light" borderRadius="3xl" p={4} my={4}>
          {x + 1}. Lorem ipsum dolor, sit amet consectetur adipisicing elit.
          Quasi sit voluptatem aliquam voluptates quos nam, voluptatum incidunt
          error explicabo harum doloremque dolore dignissimos, rerum soluta. Nam
          voluptatem nisi earum tenetur.
        </Box>
      ))}
    </>
  );
};

export default Dashboard;
