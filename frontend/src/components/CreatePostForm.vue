<template>
  <PostCard>
    <div class="d-flex">
      <div class="profile_pic_colum pr-2">
        <v-avatar size="48" color="grey">
          <img :src="loggedInUser.profilePicture" alt="">
        </v-avatar>
      </div>
      <v-form ref="newPostForm" class="d-inline-block flex-grow-1">
        <v-textarea v-model="newPostMessage" auto-grow rows="1" flat solo hide-details
                    :label="greetingsStore.currentGreeting"></v-textarea>
      </v-form>
      <v-btn @click="addNewPost">Post</v-btn>
    </div>
  </PostCard>
</template>

<script>
import PostCard from '@/components/PostCard'
import { usePostStore } from '@/store/postStore'
import { useGreetingsStore } from '@/store/greetingStore'

export default {
  name: 'CreatePostForm',
  components: { PostCard },

  setup(){
    return {
      postStore: usePostStore(),
      greetingsStore: useGreetingsStore()
    }
  },
  computed: {},

  data: () => ({
    newPostMessage: '',
    loggedInUser: { profilePicture: '' }
  }),

  methods: {
    async addNewPost() {
      await this.postStore.createPost(this.newPostMessage)
      this.greetingsStore.randomize()
      this.$refs.newPostForm.reset()
    },
  },
}
</script>

<style scoped>

</style>
