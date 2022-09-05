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
              <router-link style="color: inherit" to="logout">
                <v-hover v-slot="{ hover }">
                  <div :style="{ 'background-color': hover? '#f0f0f0' : 'inherit' }"
                       class="py-3 text-center" @click>
                    Log out @{{ authStore.currentUser.username }}
                  </div>
                </v-hover>
              </router-link>
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
  </v-app>
</template>

<script>


import { useAuthStore } from '@/store/authStore'

export default {
  setup () {
    return {
      authStore: useAuthStore()
    }
  },
  data: () => ({
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
