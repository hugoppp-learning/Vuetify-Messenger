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
            <CreatePostForm/>
            <Post v-for="post in postStore.posts" :key="post.id"
                  :model="post"
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
import Post from '@/components/Post'
import CreatePostForm from '@/components/CreatePostForm'
import { usePostStore } from '@/store/postStore'
import { useAuthStore } from '@/store/authStore'

export default {
  name: 'Home',
  components: {
    CreatePostForm,
    Post,
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

  },

  data: () => ({
    newPost: '',
  }),
}
</script>
