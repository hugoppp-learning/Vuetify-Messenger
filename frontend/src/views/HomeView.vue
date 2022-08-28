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
          <v-list>
            <CreatePostForm @newPostCreated="m => addNewPost(m)"/>
            <Post v-for="post in posts" :key="post.id"
                  :likes="post.likes"
                  :liked="post.liked"
                  :message="post.message"
                  :profile-picture="post.profilePicture"
                  :user-name="post.name"
            >
            </Post>
          </v-list>
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
.action_icon_container {
  display: flex;
}

.post_colum {
  width: 100%;
}
</style>

<script>
import HelloWorld from '../components/HelloWorld'
import Post from '@/components/Post'
import CreatePostForm from '@/components/CreatePostForm'

export default {
  name: 'Home',

  components: {
    CreatePostForm,
    Post,
    HelloWorld
  },

  methods: {

    addNewPost (message) {
      let newPost = {
        id: Math.max(...this.posts.map(p => p.id)) + 1,
        name: 'hugop',
        profilePicture: 'https://i.pravatar.cc/300',
        message: message,
        likes: 0,
        liked: true
      }
      this.posts.unshift(newPost)
      this.$refs.newPostForm.reset()
      console.log(newPost)
    }
  },

  data: () => ({
    newPost: '',
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
