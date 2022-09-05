<template>
  <v-app id="inspire">
    <template v-if="!$route.path.includes('login')">
      <v-app-bar
        app
        color="white"
        flat
      >
        <v-avatar
          :color="$vuetify.breakpoint.smAndDown ? 'grey darken-1' : 'transparent'"
          size="32"
        ></v-avatar>
        <v-container class="hidden-sm-and-down mr-15"/>

        <v-spacer/>

        <v-tabs
          centered
          class="ml-n9"
          color="grey darken-1"
        >
          <v-tab
            v-for="link in links"
            :key="link.to"
            :to="link.to"
          >
            {{ link.text }}
          </v-tab>
        </v-tabs>

        <v-spacer></v-spacer>

        <v-container
          class="appbar-left-right-container hidden-sm-and-down overflow-hidden text-no-wrap ml-15 mr-0">
          <router-link to="/profile">@{{ authStore.currentUser.username }}</router-link>
        </v-container>

        <v-menu content-class="rounded-xl" offset-y>
          <template v-slot:activator="{on, attrs}">

            <v-avatar
              v-on="on"
              v-bind="attrs"
              class="hidden-sm-and-down"
              color="grey darken-1 shrink"
              size="32"
            >
              <img :src="authStore.currentUser.profilePicture" alt="">

            </v-avatar>
          </template>
          <v-card width="200">
            <v-container class="py-1 px-0">
              <v-hover v-slot="{ hover }">
                <div :style="{ 'background-color': hover? '#f0f0f0' : 'inherit' }"
                     class="py-3 text-center" @click>
                  Settings
                </div>
              </v-hover>
              <v-hover v-slot="{ hover }">
                <div :style="{ 'background-color': hover? '#f0f0f0' : 'inherit' }"
                     class="py-3 text-center" @click="confirmLogoutOverlayOpen = true">
                  Log out @{{ authStore.currentUser.username }}
                </div>
              </v-hover>
            </v-container>
          </v-card>
        </v-menu>
      </v-app-bar>

      <v-main class="grey lighten-3">
        <router-view></router-view>
      </v-main>
    </template>
    <template v-else>
      <router-view></router-view>
    </template>
    <v-dialog content-class="rounded-xl" close-delay="1175" width="300" v-model="confirmLogoutOverlayOpen">
      <v-card>
        <v-card-title>Log out?</v-card-title>
        <v-card-text class="pb-0">
          You can always log back in at any time.
        </v-card-text>
        <v-container class="px-5 pb-5">
          <v-btn rounded="rounded" block color="red" class="my-2 white--text" @click="logout()">Log out</v-btn>
          <v-btn rounded="rounded" block color="primary" class="ma-y" @click="confirmLogoutOverlayOpen = false">Cancel</v-btn>
        </v-container>
      </v-card>
    </v-dialog>
  </v-app>
</template>

<script>


import { useAuthStore } from '@/store/authStore'
import { router } from '@/router'

export default {
  setup () {
    return {
      authStore: useAuthStore()
    }
  },
  methods: {
    logout () {
      this.confirmLogoutOverlayOpen = false
      this.authStore.deleteToken()
      router.push({ path: '/login' })
      this.authStore.destroyUser()
    }
  },
  data: () => ({
    confirmLogoutOverlayOpen: false,
    links: [
      {
        text: 'Home',
        to: '/'
      },
      {
        text: 'Messages',
        to: ''
      },
      {
        text: 'Profile',
        to: '/profile'
      },
      {
        text: 'About',
        to: '/about'
      }
    ],
  }),
}
</script>

<style>
.appbar-left-right-container {
  text-align: right;
  text-overflow: ellipsis
}


a {
  text-decoration: none;
}
</style>
