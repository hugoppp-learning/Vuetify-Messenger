<template>
  <v-container fluid fill-height>
    <v-layout align-center justify-center>
      <v-flex xs12 sm8 md4>
        <v-card class="elevation-12">
          <v-toolbar dark color="primary">
            <v-toolbar-title>Login</v-toolbar-title>
          </v-toolbar>
          <v-card-text>
            <v-form ref="form">
              <v-text-field
                prepend-icon="mdi-account"
                name="login"
                label="Login"
                type="text"
                v-model="username"
                @keyup.enter="submit"
              ></v-text-field>
              <v-text-field
                prepend-icon="mdi-lock"
                name="password"
                label="Password"
                type="password"
                v-model="password"
                @keyup.enter="submit"
              ></v-text-field>
            </v-form>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="primary" @click="submit">Login</v-btn>
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import { useAuthStore } from '@/store/authStore'

export default {
  name: 'LoginView',
  props: {
    source: String,
  },
  setup: () => ({
    authStore: useAuthStore()
  }),
  data: () => ({
    username: '',
    password: ''
  }),
  methods: {
    async submit () {
      console.log("logging in " + this.username)
      await this.authStore.login(this.username, this.password)
    }
  }
}
</script>

<style></style>
