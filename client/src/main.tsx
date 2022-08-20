import ReactDOM from "react-dom/client";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { ChakraProvider } from "@chakra-ui/react";
import theme from "./layout/theme";
import "./layout/fonts.css";
import Layout from "./layout/Layout";
import Dashboard from "./pages/Dashboard";
import NotFound from "./pages/NotFound";

const dashboard = <Layout title="Dashboard" view={<Dashboard />} />;

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
  <BrowserRouter>
    <ChakraProvider theme={theme}>
      <Routes>
        <Route path="/" element={dashboard} />
        <Route path="/reports" element={dashboard} />
        <Route path="/investments" element={dashboard} />
        <Route path="/messages" element={dashboard} />
        <Route path="*" element={<NotFound />} />
      </Routes>
    </ChakraProvider>
  </BrowserRouter>
);
