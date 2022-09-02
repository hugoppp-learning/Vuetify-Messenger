<template>

  <v-container style="max-width: 1450px">
    <v-row>

      <v-col
        class="left_colum"
        cols="12"
        sm="2"
      >
        <div style="position: sticky; top: 76px">
          <v-sheet
            rounded="lg"
            min-height="268"
          >
            <!--  -->
          </v-sheet>
        </div>
      </v-col>

      <v-col
        class="center_colum"
        cols="12"
        sm="7"
      >
        <v-sheet
          min-height="70vh"
          rounded="lg"
        >
          <v-list>
            <CreatePostForm @newPostCreated="m => addNewPost(m)"/>
            <Post v-for="post in postStore.posts" :key="post.id"
                  :likes="post.likes"
                  :liked="post.liked"
                  :message="post.message"
                  :profile-picture="post.profilePicture"
                  :username="post.username"
            >
            </Post>
          </v-list>
        </v-sheet>
      </v-col>

      <v-col
        class="right_colum"
        cols="12"
        sm="3"
      >
        <div style="position: sticky; top: 76px">
          <v-sheet
            rounded="lg"
            min-height="268"
          >
            <!--  -->
          </v-sheet>
        </div>
      </v-col>

    </v-row>
  </v-container>

</template>

<style>
.action_icon_container {
  display: flex;
}

.post_colum {
  width: 100%;
}
</style>

<script>
import HelloWorld from '../components/HelloWorld'
import Post from '@/components/Post'
import CreatePostForm from '@/components/CreatePostForm'
import { usePostStore } from '@/store/postStore'
import { useAuthStore } from '@/store/authStore'

export default {
  name: 'Home',
  components: {
    CreatePostForm,
    Post,
    HelloWorld
  },

  setup () {
    return {
      postStore: usePostStore(),
      authStore: useAuthStore()
    }
  },

  async mounted () {
    await this.postStore.fetchPosts()
  },

  methods: {

    addNewPost (message) {
      let newPost = {
        id: Math.max(...this.posts.map(p => p.id)) + 1,
        name: 'hugop',
        profilePicture: 'https://i.pravatar.cc/300',
        message: message,
        likes: 0,
        liked: true
      }
      this.posts.unshift(newPost)
      this.$refs.newPostForm.reset()
      console.log(newPost)
    }
  },

  data: () => ({
    newPost: '',
  }),
}
</script>
