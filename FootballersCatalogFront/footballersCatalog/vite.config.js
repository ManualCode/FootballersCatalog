import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import tailwindcss from '@tailwindcss/vite'
// import { viteExternalsPlugin } from 'vite-plugin-externals'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react(),
     tailwindcss()],
})
