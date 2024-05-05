import { TanStackRouterVite } from '@tanstack/router-vite-plugin'
import react from '@vitejs/plugin-react-swc'
import { resolve } from 'path'
import { defineConfig, loadEnv } from 'vite'

export default defineConfig(({ mode }) => {
  const env = loadEnv(mode, process.cwd())
  const APP_PORT = parseInt(env.VITE_APP_PORT, 10)
  const PREVIEW_PORT = parseInt(env.VITE_PREVIEW_PORT, 10)

  return {
    plugins: [react(), TanStackRouterVite()],

    server: {
      port: APP_PORT,
      strictPort: true,
      host: true,
    },
    preview: {
      port: PREVIEW_PORT,
      strictPort: true,
    },

    resolve: {
      alias: [{ find: '@', replacement: resolve(__dirname, 'src') }],
    },
  }
})
