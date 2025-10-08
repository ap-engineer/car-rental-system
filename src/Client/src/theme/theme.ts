import { createTheme } from "@mui/material/styles";

const theme = createTheme({
    palette: {
        mode: "dark",
        background: { default: "#0b0f19", paper: "#111827" },
        primary: { main: "#2563eb" },
        text: { primary: "#ffffff", secondary: "#9ca3af" },
    },
    shape: { borderRadius: 12 },
});

export default theme;
