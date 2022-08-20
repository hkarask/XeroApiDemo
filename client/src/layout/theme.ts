import { extendTheme, theme as base } from "@chakra-ui/react";
import colors from "./colors";

const theme = extendTheme({
  semanticTokens: {
    colors: {
      secondary: {
        default: "gray.400",
        _dark: "white",
      },
      "bg-dark": {
        default: "secondaryGray.300",
        _dark: "navy.900",
      },
      "bg-light": {
        default: "white",
        _dark: "navy.800",
      },
      heading: {
        default: "navy.700",
        _dark: "white"
      },
    },
  },
  styles: {
    global: {
      body: {
        letterSpacing: "tight",
      },
    },
  },

  colors,
  fonts: {
    heading: `Montserrat, ${base.fonts?.heading}`,
    body: `"DM Sans", ${base.fonts?.body}`,
  },
});

export default theme;
