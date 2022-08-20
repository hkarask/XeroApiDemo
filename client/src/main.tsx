import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { ChakraProvider } from "@chakra-ui/react";
import theme from "./layout/theme";
import "./layout/fonts.css";
import Layout from "./layout/Layout";
import Dashboard from "./pages/Dashboard";

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
  <React.StrictMode>
    <BrowserRouter>
      <ChakraProvider theme={theme}>
        <Routes>
          <Route path="/" element={<Layout view={<Dashboard />} />} />
          <Route path="/reports" element={<Layout view={<Dashboard />} />} />
          <Route path="/investments" element={<Layout view={<Dashboard />} />} />
          <Route path="/messages" element={<Layout view={<Dashboard />} />} />
        </Routes>
      </ChakraProvider>
    </BrowserRouter>
  </React.StrictMode>
);
