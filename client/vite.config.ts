import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react-swc'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    host: '0.0.0.0',          // CRITICAL: Listen on all interfaces, not just localhost
    port: 3000,               // Use port 3000 to match our Docker setup
    watch: {
      usePolling: true        // Enable polling for file changes in Docker
    }
  }
})