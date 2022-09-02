import { defineStore } from 'pinia'

export const useGreetingsStore = defineStore('greetings', {
  state: () => {
    return { currentGreeting: getRandomGreeting() }
  },

  actions: {
    randomize () {
      this.currentGreeting = getRandomGreeting()
    }
  }

})

function getRandomGreeting () {
  return allGreetings[Math.floor(Math.random() * allGreetings.length)]
}

const allGreetings = [
  'Whats’s up?',
  'Sup?',
  'Howdy?',
  'How’s it going?',
  'What’s going on?',
  'Wagwan',
  'What’s happening?',
  'What’s new?',
  'Anything new with you?',
  'What’s new with you?',
  'How are you?',
  'Any excitement recently?',
  'What are you up to?',
  'What you up to?',
  'What you doing?',
  'Whatcha doin’?',
  'What’s cookin’?',
  'What’s shaking?',
  'What’s sizzlin’?',
  'What’s crackin’?',
  'What’s poppin’?',
  'What’s shakin’ bacon?',
  'How have you been?',
  'How you been?',
  'How’s life treating you?',
  'Hey there!',
  'What’s up buttercup?',
  'What’s cookin’, good lookin’?',
  'Well hello there!',
]
