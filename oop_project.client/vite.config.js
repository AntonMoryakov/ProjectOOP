import { fileURLToPath, URL } from "node:url";

import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";
import fs from "fs";
import path from "path";
import child_process from "child_process";
import { env } from "process";
import { VitePWA } from "vite-plugin-pwa";
import PWAManifest from "./manifest.json";
import vuetify from 'vite-plugin-vuetify'

const baseFolder =
  env.APPDATA !== undefined && env.APPDATA !== ""
    ? `${env.APPDATA}/ASP.NET/https`
    : `${env.HOME}/.aspnet/https`;

const certificateName = "oop_project.client";
const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
  if (
    0 !==
    child_process.spawnSync(
      "dotnet",
      [
        "dev-certs",
        "https",
        "--export-path",
        certFilePath,
        "--format",
        "Pem",
        "--no-password",
      ],
      { stdio: "inherit" }
    ).status
  ) {
    throw new Error("Could not create certificate.");
  }
}

if (process.env.NODE_ENV == "development") {
  PWAManifest.manifest.icons[0].src = "/src/assets/logo.svg";
}

//const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
//    env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:7139';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    vue(),
    vuetify({ autoImport: true }), // Enabled by default
    VitePWA({
      registerType: "autoUpdate",
      devOptions: {
        enabled: true,
      },
      manifest: PWAManifest.manifest,
    }),
  ],
  cssCodeSplit: true,
  rollupOptions: {
    output: {
      // Define manual chunks for dependencies
      manualChunks(id) {
        if (id.includes("node_modules")) {
          return "vendor";
        }
      },
    },
  },
  resolve: {
    alias: {
      "@": fileURLToPath(new URL("./src", import.meta.url)),
    },
  },
  server: {
    proxy: {
      "/api": {
        target: "https://localhost:5000", // ASP.NET Core API
        changeOrigin: true,
        secure: false,
      },
      "/swagger": {
        target: "https://localhost:5000", // Swagger api renderer
        changeOrigin: true,
        secure: false,
      },
    },
    port: parseInt(process.env.PORT ?? "80"),
    https: {
      key: fs.readFileSync(keyFilePath),
      cert: fs.readFileSync(certFilePath),
    },
  },
});
