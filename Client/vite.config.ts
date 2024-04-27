import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [react()],
    define: {
        'process.env': process.env,
    },
    server: {
        //host: true,
        //cors
        //VITE_JSON_BASE_URL=https://localhost:7121
        host: 'localhost',
        port: 5173,
        proxy: {
            '/api': {
            target: 'https://localhost:7121',//目标服务器地址
            changeOrigin: true,
            rewrite: (path) => path.replace(/^\/api/, '')
            },
        }
    },
    base: './',
});
