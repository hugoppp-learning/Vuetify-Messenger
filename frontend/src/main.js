import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify'
import { createPinia, PiniaVuePlugin } from 'pinia'
import { useAuthStore } from '@/store/authStore'
import axios from 'axios'
import { createRouter } from './router'

Vue.use(PiniaVuePlugin)
const pinia = createPinia()
const router = createRouter(pinia)

Vue.config.productionTip = false
axios.interceptors.request.use(function (config) {
  const token = useAuthStore(pinia).token
  if (token != null){
    console.log("Using token auth")
    config.headers.common["Authorization"] = token
  }
  return config
})

new Vue({
  router,
  pinia,
  vuetify,
  render: h => h(App)
}).$mount('#app')
