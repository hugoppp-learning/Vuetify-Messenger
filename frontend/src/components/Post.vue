<template>
  <PostCard>
    <div class="d-flex">

      <div class="profile_pic_colum pr-2">
        <v-avatar size="48">
          <img :src="profilePicture" alt="">
        </v-avatar>
      </div>

      <div class="post_colum mpt-0 px-1">
        @<span class="font-weight-bold">{{ userName }}</span>

        <div class="pl-0 py-1">{{ message }}</div>


        <div class="action_icon_container">
          <v-icon @click="toggleLike()" class="mr-2">
            {{ liked ? 'mdi-heart' : 'mdi-heart-outline' }}
          </v-icon>
          <span>{{ likes }}</span>

          <v-spacer/>

          <v-hover v-slot="{hover}" close-delay="100" open-delay="200">
            <v-icon class="mr-2">
              {{ hover ? 'mdi-delete' : 'mdi-delete-outline' }}
            </v-icon>
          </v-hover>

          <v-spacer/>

          <v-icon class="mr-2">
            mdi-share
          </v-icon>

          <v-spacer/>

          <v-icon class="mr-2">
            mdi-menu
          </v-icon>

        </div>


      </div>
    </div>
  </PostCard>
</template>

<script>
import PostCard from '@/components/PostCard'

export default {
  name: 'Post',
  components: { PostCard },
  props: [
    'liked',
    'profilePicture',
    'userName',
    'message',
    'likes',
  ],
  methods: {
    toggleLike () {
      if (this.liked) {
        this.likes--
      } else {
        this.likes++
      }
      this.liked = !this.liked
    }
  }

}
</script>

<style scoped>

</style>
