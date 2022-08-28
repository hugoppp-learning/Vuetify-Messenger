<template>

  <v-container style="max-width: 1450px">
    <v-row>
      <v-col
        class="left_colum"
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
        class="center_colum"
        cols="12"
        sm="7"
      >
        <v-sheet
          min-height="70vh"
          rounded="lg"
        >
          <!--  -->

          <v-list>
            <v-card v-for="post in posts" class="ma-5 pa-3">

              <div class="d-flex">

                <div class="profile_pic_colum pr-2">
                  <v-avatar size="48">
                    <img :src="post.profilePicture" alt="">
                  </v-avatar>
                </div>

                <div class="post_colum mpt-0 px-1">
                  @<span class="font-weight-bold">{{ post.name }}</span>

                  <div class="pl-0 py-1">{{ post.message }}</div>


                  <div class="action_icon_container">
                    <v-icon @click="toggleLike(post)" class="mr-2">
                      {{ post.liked ? 'mdi-heart' : 'mdi-heart-outline' }}
                    </v-icon>
                    <span>{{ post.likes }}</span>

                    <v-spacer/>

                    <v-hover  v-slot="{hover}" close-delay="100" open-delay="200">
                      <v-icon class="mr-2">
                        {{ hover ? 'mdi-delete' : 'mdi-delete-outline' }}
                      </v-icon>
                    </v-hover>

                    <v-spacer/>

                    <v-icon class="mr-2">
                      mdi-share
                    </v-icon>

                  </div>


                </div>
              </div>
            </v-card>


          </v-list>


          <!--  -->
        </v-sheet>
      </v-col>

      <v-col
        class="right_colum"
        cols="12"
        sm="3"
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

<style>
.action_icon_container{
  display: flex;
}
.post_colum{
  width: 100%;
}
</style>

<script>
import HelloWorld from '../components/HelloWorld'

export default {
  name: 'Home',

  components: {
    HelloWorld
  },
  methods: {
    toggleLike (post) {
      if (post.liked) {
        post.likes--
      } else {
        post.likes++
      }
      post.liked = !post.liked
    }
  },

  data: () => ({
    posts: [
      {
        id: 1,
        name: 'hugop',
        profilePicture: 'https://i.pravatar.cc/300',
        message: 'This is the first post on this site, Yay!! :)',
        likes: 12,
        liked: true
      },
      {

        id: 2,
        name: 'hugop',
        profilePicture: 'https://i.pravatar.cc/300',
        message: 'This is the second post on this site',
        likes: 1
      },
      {
        id: 3,
        name: 'hugop',
        profilePicture: 'https://i.pravatar.cc/300',
        message: `Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum`,
        likes: 0
      },
      {
        id: 4,
        name: 'hugop',
        profilePicture: 'https://i.pravatar.cc/300',
        message: `Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum`,
        likes: 0
      },
      {
        id: 5,
        name: 'hugop',
        profilePicture: 'https://i.pravatar.cc/300',
        message: `Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum`,
        likes: 0
      },
      {
        id: 6,
        name: 'hugop',
        profilePicture: 'https://i.pravatar.cc/300',
        message: `Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum`,
        likes: 0
      },
      {
        id: 7,
        name: 'hugop',
        profilePicture: 'https://i.pravatar.cc/300',
        message: `Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum`,
        likes: 0
      },
    ]
  }),
}
</script>
