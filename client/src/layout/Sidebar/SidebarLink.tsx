import { Link, Icon, useColorModeValue, Box, Flex } from "@chakra-ui/react";
import { NavLink } from "react-router-dom";
import { IconType } from "react-icons";

const SidebarLink = ({
  icon,
  text,
  path,
}: {
  icon: IconType;
  text: string;
  path: string;
}) => {
  const activeColor = useColorModeValue("gray.700", "white");
  const inactiveColor = useColorModeValue("secondaryGray.500", "gray.400");
  const iconColor = useColorModeValue("brand.500", "white");

  return (
    <Flex role="sidebar-link" w="full" h={10} py={1} alignItems="center">
      <Link
        as={NavLink}
        display="flex"
        variant="active"
        color={inactiveColor}
        letterSpacing="tight"
        alignItems="center"
        transitionDuration="slow"
        _activeLink={{
          color: activeColor,
          fontWeight: "bold",
          "& + .active-marker": {
            visibility: "visible",
          },
        }}
        _hover={{ textDecor: "none", color: "gray.300" }}
        to={path}
        width="full"
      >
        <Icon mr={3} as={icon} color={iconColor} />
        {text}
      </Link>
      <Box
        className="active-marker"
        bg={iconColor}
        w={1}
        h="full"
        borderRadius="5px"
        ml="auto"
        visibility="hidden"
      />
    </Flex>
  );
};

export default SidebarLink;
