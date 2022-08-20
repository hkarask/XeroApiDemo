import {
  Flex,
  Heading,
  Icon,
  Spacer,
  useColorModeValue,
} from "@chakra-ui/react";
import { IoMenuOutline } from "react-icons/io5";
import TopMenu from "./TopMenu";

const Header = () => {
  const showSidebar = true;
  let bgColor = useColorModeValue(
    "rgba(244, 247, 254, 0.2)",
    "rgba(11,20,55,0.5)"
  );

  return (
    <Flex role="header" bg={bgColor} alignItems="center" pos="sticky" top={0} py={6} px={8} pb={3} backdropFilter="blur(20px)">
      <Icon
        display={showSidebar ? "none" : "inherit"}
        role="show-sidebar"
        // onClick={toggleSidebar}
        as={IoMenuOutline}
        color="secondary"
        my="auto"
        me="10px"
        w="20px"
        h="20px"
        _hover={{ cursor: "pointer" }}
      />
      <Heading size={["md", "lg"]} color="heading" role="title">
        Dashboard
      </Heading>
      <Spacer />
      <TopMenu />
    </Flex>
  );
};

export default Header;
