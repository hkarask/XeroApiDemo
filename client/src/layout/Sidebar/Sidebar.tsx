import { VStack, Flex } from "@chakra-ui/react";
import { FiBriefcase, FiMessageCircle, FiTrello, FiZap } from "react-icons/fi";

import Brand from "./Brand";
import SidebarLink from "./SidebarLink";

const Sidebar = () => {
  return (
    <Flex h="full" bg="bg-light" direction="column" role="sidebar">
      <Brand />
      <VStack w="full" p={6} pr={0} role="side-menu">
        <SidebarLink icon={FiZap} text="Dashboard" path="/" />
        <SidebarLink icon={FiTrello} text="Reports" path="/reports" />
        <SidebarLink icon={FiBriefcase} text="Portfolio" path="/investments" />
        <SidebarLink icon={FiMessageCircle} text="Messages" path="/messages" />
      </VStack>
    </Flex>
  );
};

export default Sidebar;
