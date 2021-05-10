import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import { rootStore } from '@/store';
import './assets/styles/tailwind.css';

createApp(App).use(router).use(rootStore).mount('#app');
