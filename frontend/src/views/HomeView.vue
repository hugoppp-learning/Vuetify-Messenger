<template>

  <v-container>
    <v-row>
      <v-col
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
        cols="12"
        sm="8"
      >
        <v-sheet
          min-height="70vh"
          rounded="lg"
        >
          <!--  -->

          <v-list>
            <v-card v-for="post in posts" class="ma-5 pa-3">

              <div class="d-flex">

                <div class="pr-2">
                  <v-avatar size="48">
                    <img :src="post.profilePicture" alt="">
                  </v-avatar>
                </div>

                <div class="pt-1 px-1">
                  @<span class="font-weight-bold">{{ post.name }}</span>

                  <div class="pl-0 py-1">{{ post.message }}</div>


                  <div class="d-flex" style="align-items: center">
                    <v-icon class="mr-2">mdi-heart-outline</v-icon>
                    <span>{{ post.likes }}</span>
                  </div>
                </div>
              </div>
            </v-card>


          </v-list>


          <!--  -->
        </v-sheet>
      </v-col>

      <v-col
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
    </v-row>
  </v-container>

</template>

<script>
import HelloWorld from '../components/HelloWorld'

export default {
  name: 'Home',

  components: {
    HelloWorld
  },

  data: () => ({
    posts: [
      {
        name: 'hugop',
        profilePicture: 'https://i.pravatar.cc/300',
        message: 'This is the first post on this site, Yay!! :)',
        likes: 12
      },
      {
        name: 'hugop',
        profilePicture: 'https://i.pravatar.cc/300',
        message: 'This is the second post on this site',
        likes: 1
      },
      {
        name: 'hugop',
        profilePicture: 'https://i.pravatar.cc/300',
        message: `Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum`,
        likes: 0
      },
      {
        name: 'hugop',
        profilePicture: 'https://i.pravatar.cc/300',
        message: `Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum`,
        likes: 0
      },
      {
        name: 'hugop',
        profilePicture: 'https://i.pravatar.cc/300',
        message: `Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum`,
        likes: 0
      },
      {
        name: 'hugop',
        profilePicture: 'https://i.pravatar.cc/300',
        message: `Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum`,
        likes: 0
      },
      {
        name: 'hugop',
        profilePicture: 'https://i.pravatar.cc/300',
        message: `Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum`,
        likes: 0
      },
    ]
  }),
}
</script>
