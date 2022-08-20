import { Divider, Text, Box, Icon } from "@chakra-ui/react";
import { BiDizzy } from "react-icons/bi";

const Brand = () => {
  return (
    <Box w="full" marginTop={5} textAlign="center" role="brand">
      <Text fontSize={30} p={6} fontWeight="bold" color="heading">
        <Icon as={BiDizzy} mr={2} top={1} position="relative" />
        XERO DEMO
      </Text>
      <Divider />
    </Box>
  );
};

export default Brand;
