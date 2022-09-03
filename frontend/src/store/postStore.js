import { defineStore } from 'pinia'
import axios from 'axios'

const resource_uri = 'http://localhost:5277/Post'

export const usePostStore = defineStore('post', {
  state: () => {
    return { posts: [] }
  },

  actions: {
    async fetchPosts () {
      const response = await axios.get(resource_uri)
      this.posts = response.data
    },
    async createPost (message) {
      const response = await axios.post(resource_uri, { message });
      const post = response.data;
      this.posts.unshift(post)
      return post
    },
    async likePost (post) {
      console.log(post)
      await axios.post(`${resource_uri}/${post.id}/like`);
      post.likes++
      post.liked = true;
    },
    async unlikePost (post) {
      await axios.delete(`${resource_uri}/${post.id}/like`);
      post.likes--
      post.liked = false;
    },
    async deletePost (id) {
      this.posts = this.posts.filter(p => p.id !== id)
      await axios.delete(`${resource_uri}/${id}`);
    }
  },

  getters: {}
})
