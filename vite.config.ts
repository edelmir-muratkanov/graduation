import { TanStackRouterVite } from '@tanstack/router-vite-plugin'
import react from '@vitejs/plugin-react-swc'
import { resolve } from 'path'
import { defineConfig, loadEnv } from 'vite'

export default defineConfig(({ mode }) => {
  const env = loadEnv(mode, process.cwd())
  const API_URL = env.VITE_SERVER_URL ?? ''
  const APP_PORT = parseInt(env.VITE_APP_PORT, 10)

  return {
    plugins: [react(), TanStackRouterVite()],

    server: {
      port: APP_PORT,
      proxy: {
        '/api': API_URL,
      },
    },

    resolve: {
      alias: [{ find: '@', replacement: resolve(__dirname, 'src') }],
    },
  }
})
