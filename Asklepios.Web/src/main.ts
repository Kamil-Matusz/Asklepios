import './assets/main.css'
import '@mdi/font/css/materialdesignicons.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'
import { aliases, mdi } from 'vuetify/iconsets/mdi'

// Vuetify
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'

import pl from 'vuetify/lib/locale/pl'

import Toast from "vue-toastification";
import "vue-toastification/dist/index.css";

import i18n from './translator';

import * as signalR from '@microsoft/signalr';

const customTheme = {
  dark: false,
  colors: {
    background: '#202020',
    surface: '#202020',
    'surface-bright': '#FFFFFF',
    'surface-light': '#EEEEEE',
    'surface-variant': '#424242',
    'on-surface-variant': '#EEEEEE',
    primary: '#ffca00',
    'primary-darken-1': '#1F5592',
    secondary: '#48A9A6',
    'secondary-darken-1': '#018786',
    error: '#B00020',
    info: '#2196F3',
    success: '#4CAF50',
    warning: '#FB8C00',
  },
  variables: {
    'border-color': '#000000',
    'border-opacity': 0.12,
    'high-emphasis-opacity': 0.87,
    'medium-emphasis-opacity': 0.60,
    'disabled-opacity': 0.38,
    'idle-opacity': 0.04,
    'hover-opacity': 0.04,
    'focus-opacity': 0.12,
    'selected-opacity': 0.08,
    'activated-opacity': 0.12,
    'pressed-opacity': 0.12,
    'dragged-opacity': 0.08,
    'theme-kbd': '#212529',
    'theme-on-kbd': '#FFFFFF',
    'theme-code': '#F5F5F5',
    'theme-on-code': '#000000',
  }
}



const vuetify = createVuetify({
  components,
  directives,
  theme: {
    defaultTheme: 'customTheme',
    themes: {
      customTheme,
    },
  },
  icons: {
    defaultSet: 'mdi',
    aliases,
    sets: {
      mdi
    }
  },
  locale: {
    locale: 'pl',
    fallback: 'en',
    messages: { pl },
  }
})

const connection = new signalR.HubConnectionBuilder()
  .withUrl("http://localhost:5102/hospitalHub")
  .build();

connection.start()
  .then(() => {
    console.log("SignalR Connected");
  })
  .catch(err => console.error("SignalR Connection Error: ", err));

const app = createApp(App)

app.use(vuetify).use(createPinia())
app.use(router)

app.use(Toast);

app.use(i18n);

app.mount('#app')
