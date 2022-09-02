import { defineStore } from 'pinia'
import axios from 'axios'
import post from '@/components/Post'

const resource_uri = 'http://localhost:5277/Post'

export const usePostStore = defineStore('post', {
  state: () => {
    return { posts: [] }
  },

  actions: {
    async fetchPosts () {
      const response = await axios.get(resource_uri)
      this.posts = response.data;
    },
  },

  getters: {
  }
})
