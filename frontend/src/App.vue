<template>
  <v-app id="inspire">
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
          :key="link"
          :to="link.to"
        >
          {{ link.text }}
        </v-tab>
      </v-tabs>

      <v-spacer></v-spacer>

      <v-container
        class="appbar-left-right-container hidden-sm-and-down overflow-hidden text-no-wrap ml-15 mr-0">
        <router-link to="/profile">@username</router-link>
      </v-container>

      <router-link to="/profile">
        <v-avatar
          class="hidden-sm-and-down"
          color="grey darken-1 shrink"
          size="32"
        ></v-avatar>
      </router-link>
    </v-app-bar>

    <v-main class="grey lighten-3">
      <router-view></router-view>
    </v-main>
  </v-app>
</template>

<script>
export default {
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
