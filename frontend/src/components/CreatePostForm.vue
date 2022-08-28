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
                    label="What's up?"></v-textarea>
      </v-form>
      <v-btn @click="createPost()">Post</v-btn>
    </div>
  </PostCard>
</template>

<script>
import PostCard from '@/components/PostCard'
import { mapGetters } from 'vuex'

export default {
  name: 'CreatePostForm',
  components: { PostCard },

  computed: {
    ...mapGetters({
      loggedInUser: 'getLoggedInUser'
    }),
  },

  emits: ['newPostCreated'],
  data: () => ({
    newPostMessage: ''
  }),

  methods: {
    createPost () {

      this.$emit('newPostCreated', this.newPostMessage)
    },
  },
}
</script>

<style scoped>

</style>
