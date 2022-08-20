import { Button, Icon, useColorMode } from "@chakra-ui/react";
import { IoMdMoon, IoMdSunny } from "react-icons/io";

const ToggleDarkmode = () => {
  const { colorMode, toggleColorMode } = useColorMode();

  return (
    <Button
      variant="no-hover"
      bg="transparent"
      p="0px"
      minW="unset"
      minH="unset"
      h="18px"
      w="max-content"
      onClick={toggleColorMode}
    >
      <Icon
        me="10px"
        h="18px"
        w="18px"
        color="secondary"
        as={colorMode === "light" ? IoMdMoon : IoMdSunny}
      />
    </Button>
  );
};

export default ToggleDarkmode;
