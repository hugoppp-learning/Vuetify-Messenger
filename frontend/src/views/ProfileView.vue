<template>
  <v-container style="max-width: 1450px">
    <v-row>

      <v-col class="left_colum" cols="12" sm="2">
        <div style="position: sticky; top: 76px">
          <v-sheet
            rounded="lg"
            min-height="268"
          >
            <!--  -->
          </v-sheet>
        </div>
      </v-col>


      <v-col class="center_colum" cols="12" sm="7">
        <v-sheet min-height="70vh" rounded="lg">
          <div class="d-block text-center justify-center" style="background-color: cornflowerblue;" >
            <v-img height="250px" style="background-color: darkblue" :src="authStore.currentUser.banner" alt="banner"/>
            <div style="">profile picture and bio</div>
          </div>
          <v-list>
            <Post v-for="post in postStore.ownPosts" :key="post.id"
                  :model="post"
            >
            </Post>
          </v-list>
        </v-sheet>
      </v-col>

      <v-col class="right_colum" cols="12" sm="3">
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

<script>
import { usePostStore } from '@/store/postStore'
import { useAuthStore } from '@/store/authStore'
import Post from '@/components/Post'
import PostCard from '@/components/PostCard'

export default {
  name: 'Profile',
  components: {
    PostCard,
    Post,
  },
  setup () {
    return {
      postStore: usePostStore(),
      authStore: useAuthStore(),
    }
  }
}
</script>

<style scoped>

</style>
