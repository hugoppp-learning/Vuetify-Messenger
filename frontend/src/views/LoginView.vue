<template>
  <v-row justify="center" align="center" class="ma-2">
    <v-card class="elevation-12 limit_width" width="400">
      <v-toolbar dark color="primary">
        <v-toolbar-title>Login</v-toolbar-title>
      </v-toolbar>
      <v-card-text>
        <v-form ref="form" v-model="valid">
          <v-text-field
            prepend-icon="mdi-account"
            required
            name="login"
            label="Login"
            type="text"
            v-model="username"
            :rules="usernameRules"
            @keyup.enter="submit"
          ></v-text-field>
          <v-text-field
            prepend-icon="mdi-lock"
            required
            name="password"
            label="Password"
            type="password"
            v-model="password"
            :error-messages="invalidCredentialsMsg"
            :rules="passwordRules"
            @keyup.enter="submit"
          ></v-text-field>
        </v-form>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="primary" @click="submit" :disabled="!this.valid">Login</v-btn>
      </v-card-actions>
    </v-card>
  </v-row>
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
    valid: false,
    username: '',
    usernameRules: [v => !!v,],
    password: '',
    passwordRules: [v => !!v,],
    wrongPassword: false
  }),
  methods: {
    async submit () {
      if (!this.valid) {
        return
      }
      if (!await this.authStore.login(this.username, this.password)) {
        this.password = ''
        this.wrongPassword = true
      }
    }
  },
  computed: {
    invalidCredentialsMsg: function () {
      console.log(this.wrongPassword)
      if (this.wrongPassword && !this.password) {
        this.wrongPassword = false;
        return "Invalid credentials";
      }
      else
        return "";
    },
  }
}
</script>

<style></style>
