import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import tailwindcss from "@tailwindcss/vite";

// Proxy /api to your HTTPS backend. Adjust target port if different.
export default defineConfig({
  plugins: [react(), tailwindcss()],
  server: {
    proxy: {
      "/api": {
        target: "http://localhost:5299",
        changeOrigin: true,
        secure: false,
      },
    },
  },
});
