import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import { rootStore } from '@/store';
import './assets/styles/tailwind.css';
import { auth } from '@/plugins/firebase';

let app: any;

auth.onAuthStateChanged(() => {
  if (!app) {
    app = createApp(App).use(router).use(rootStore).mount('#app');
  }
});
