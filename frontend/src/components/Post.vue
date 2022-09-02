<template>
  <PostCard>
    <div class="d-flex">

      <div class="profile_pic_colum pr-2">
        <v-avatar size="48">
          <img :src="model.profilePicture" alt="">
        </v-avatar>
      </div>

      <div class="post_colum mpt-0 px-1">
        @<span class="font-weight-bold">{{ model.username }}</span>

        <div class="pl-0 py-1">{{ model.message }}</div>


        <div class="action_icon_container">
          <v-icon
            :disabled="this.isMyPost()"
            @click="toggleLike()"
            class="mr-2"
          >
            {{ isLiked() ? 'mdi-heart' : 'mdi-heart-outline' }}
          </v-icon>
          <span>{{ model.likes }}</span>

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
import { useAuthStore } from '@/store/authStore'
import { usePostStore } from '@/store/postStore'

export default {

  setup () {
    return {
      authStore: useAuthStore(),
      postStore: usePostStore()
    }
  },

  name: 'Post',
  components: { PostCard },
  props: [
    'model'
  ],
  methods: {
    isMyPost () {
      return this.model.username === this.authStore.currentUser.username
    },
    async toggleLike () {
      if (this.model.liked) {
        await this.postStore.unlikePost(this.model.id)
        this.model.likes--
      } else {
        await this.postStore.likePost(this.model.id)
        this.model.likes++
      }
      this.model.liked = !this.model.liked
    },
    isLiked () {
      return this.model.liked ||
        this.model.username === this.authStore.currentUser.username

    }
  }

}
</script>

<style scoped>

</style>
