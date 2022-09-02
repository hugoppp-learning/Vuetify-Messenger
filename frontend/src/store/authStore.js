import { defineStore } from 'pinia'
import axios from 'axios'
import { router } from '@/router'

const resource_uri = 'http://localhost:5277/'

export const useAuthStore = defineStore('auth', {
  state: () => ({
    currentUser: JSON.parse(localStorage.getItem('user')) ?? {},
    returnUrl: null,
  }),

  actions: {
    async login (username, password) {
      const response = await axios.post(resource_uri + 'auth', {
        username,
        password
      })
      if (response.data.code !== 'Ok') {
        return false
      }

      this.currentUser.token = 'Bearer ' + response.data.token
      this.currentUser = { ...this.currentUser, ...(await axios.get(resource_uri + 'user/current')).data }
      localStorage.setItem('user', JSON.stringify(this.currentUser));
      console.log({"logged in user" : this.currentUser})
      await router.push(this.returnUrl || '/')
      return true
    },
    async register (email, username, password) {
      const response = await axios.post(resource_uri + 'register', {
        username,
        password
      })
    },
  },

  getters: {
    isAuth: (state) => state.currentUser.token != null
  }
})
