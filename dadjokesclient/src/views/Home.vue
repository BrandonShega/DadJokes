<template>
  <div class="home">
    <div class="joke">
      {{ joke }}
    </div>
    <div class="new-joke-button">
      <b-button variant="success" @click="getJoke()">Get a new Joke</b-button>
    </div>
  </div>
</template>

<style scoped>
.home {
  display: flex;
  flex-direction: column;
  align-content: center;
  justify-content: center;
}
.new-joke-button {
  margin-top: 20px;
}
</style>

<script>
import request from 'request'

export default {
  data () {
    return {
      joke: ''
    }
  },
  mounted () {
    this.getJoke()
  },
  methods: {
    getJoke () {
      request('https://localhost:5001/api/jokes', (error, response, body) => {
        if (error) console.log(error)
        console.log(body)
        const json = JSON.parse(body)
        this.joke = json.joke
      })
    }
  }
}
</script>
