<template>
  <PostCard>
    <div style="z-index: 10" class="d-flex">

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
            :disabled="isMyPost()"
            @click="toggleLike()"
            class="mr-2"
          >
            {{ isLiked() ? 'mdi-heart' : 'mdi-heart-outline' }}
          </v-icon>
          <span>{{ model.likes }}</span>

          <v-spacer v-if="isMyPost()"/>
          <v-hover v-if="isMyPost()" v-slot="{hover}" close-delay="75" open-delay="75">
            <v-icon @click="confirmDialogOpen = true" :color="(hover || confirmDialogOpen) ? 'red' : ''" class="mr-2">
              {{ (confirmDialogOpen) ? 'mdi-delete' : 'mdi-delete-outline' }}
            </v-icon>
          </v-hover>
          <v-dialog content-class="rounded-xl" close-delay="1175" width="300" v-model="confirmDialogOpen">
            <v-card >
              <v-card-title>Delete Post?</v-card-title>
              <v-card-text class="pb-0">
                This can’t be undone and it will be removed from your profile, the homepage and from search results.
              </v-card-text>
              <v-container class="px-5 pb-5">
                <v-btn rounded="rounded" block color="red" class="my-2 white--text" @click="deletePost()">Delete</v-btn>
                <v-btn rounded="rounded" block color="primary" class="ma-y" @click="confirmDialogOpen = false">Cancel</v-btn>
              </v-container>
            </v-card>
          </v-dialog>

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
  data: () => ({ confirmDialogOpen: false }),
  methods: {
    isMyPost () {
      return this.model.username === this.authStore.currentUser.username
    },
    async toggleLike () {
      if (this.model.liked) {
        await this.postStore.unlikePost(this.model)
      } else {
        await this.postStore.likePost(this.model)
      }
    },
    async deletePost () {
      await this.postStore.deletePost(this.model.id)
      this.confirmDialogOpen = false
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
