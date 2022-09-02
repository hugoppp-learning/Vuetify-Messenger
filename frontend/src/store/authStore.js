import { defineStore } from 'pinia'
import axios from 'axios'
import { router } from '@/router'

const resource_uri = 'http://localhost:5277/'

export const useAuthStore = defineStore('auth', {
  state: () => ({
    currentUser: null,
    returnUrl: null,
    token: null,
  }),

  actions: {
    async login (username, password) {
      const response = await axios.post(resource_uri + 'auth', {
        username,
        password
      })
      if (response.data.code === 'Ok') {
        this.token = 'Bearer ' + response.data.token
        this.currentUser = (await axios.get(resource_uri + 'user/current')).data
        await router.push(this.returnUrl || '/')
      }
    },
    async register (email, username, password) {
      const response = await axios.post(resource_uri + 'register', {
        username,
        password
      })
    },
  },

  getters: {
    getToken: (state) => state.token,
    isAuth: (state) => state.token != null
  }
})
