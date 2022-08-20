import { Box } from "@chakra-ui/react";
import React from "react";
import Footer from "./Footer";
import Header from "./Header";
import Sidebar from "./Sidebar";

const Layout = ({ view }: { view: React.ReactNode }) => {
  const showSidebar = true;
  const sidebarWidth = "300px";

  return (
    <>
      <Box
        role="side-container"
        w={sidebarWidth}
        left={showSidebar ? 0 : `-${sidebarWidth}`}
        overflow="hidden"
        position="fixed"
        height="full"
        transition="all 0.6s"
        zIndex="docked"
        shadow={{
          base: showSidebar ? "0 0 16px #8d8989" : "unset",
          md: "unset",
        }}
      >
        <Sidebar />
      </Box>
      <Box
        role="main-container"
        bg="bg-dark"
        //p={8}
        //pt={4}
        pos="relative"
        left={{ base: 0, md: showSidebar ? sidebarWidth : 0 }}
        width={{
          base: "100%",
          md: showSidebar ? `calc(100% - ${sidebarWidth})` : "100%",
        }}
        transition="all 0.6s"
      >
        <Header />
        <Box p={8} pt={0}>
          {view}
        </Box>
        <Footer />
      </Box>
    </>
  );
};

export default Layout;
