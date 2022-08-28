import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    loggedInUser: {
      username: 'logInUser',
      profilePicture: 'https://cdn.mdr.de/wissen/affen-lachen-100-resimage_v-variantSmall1x1_w-256.jpg?version=57198'
    }
  },
  getters: {
    getLoggedInUser: state => state.loggedInUser
  },
  mutations: {},
  actions: {},
  modules: {}
})
