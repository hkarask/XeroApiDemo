import { HStack } from "@chakra-ui/react";
import Avatar from "./Avatar";
import ToggleDarkmode from "./ToggleDarkMode";

const TopMenu = () => {
  return (
    <HStack
      role="top-menu"
      bg="bg-light"
      align="center"
      borderRadius="3xl"
      px={4}
      py={2}
    >
      <ToggleDarkmode />
      <Avatar />
    </HStack>
  );
};

export default TopMenu;
