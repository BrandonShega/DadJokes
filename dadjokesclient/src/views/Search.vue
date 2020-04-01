<template>
  <div class="search">
    <div class="search-form">
      <b-form-input v-model="searchText" placeholder="Search Term" @keydown.enter="search()"></b-form-input>
      <b-button variant="success" @click="search()">Search</b-button>
    </div>
    <div class="results">
      <div v-if="short.length">
        <caption>Small:</caption>
        <b-list-group>
          <b-list-group-item v-for="joke in short" :key="joke.id" v-html="joke.joke"></b-list-group-item>
        </b-list-group>
      </div>
      <div v-if="medium.length">
        <caption>Medium:</caption>
        <b-list-group>
          <b-list-group-item v-for="joke in medium" :key="joke.id" v-html="joke.joke"></b-list-group-item>
        </b-list-group>
      </div>
      <div v-if="long.length">
        <caption>Long:</caption>
        <b-list-group>
          <b-list-group-item v-for="joke in long" :key="joke.id" v-html="joke.joke"></b-list-group-item>
        </b-list-group>
      </div>
      <div v-if="!short.length && !medium.length && !long.length && searched" class="no-results">
        <p>No results found!</p>
      </div>
    </div>
  </div>
</template>

<style scoped>
.search-form {
  display: flex;
  flex-direction: row;
  max-width: 500px;
  margin: 0 auto;
}
.no-results {
  margin-top: 50px;
}
</style>

<script>
import request from 'request'

export default {
  data () {
    return {
      searchText: '',
      searched: false,
      short: [],
      medium: [],
      long: []
    }
  },
  methods: {
    search () {
      request(`https://localhost:5001/api/jokes/${this.searchText}`, (error, response, body) => {
        if (error) console.log(error)
        this.searched = true
        const json = JSON.parse(body)
        this.short = json.short.map(joke => {
          return {
            id: joke.id,
            joke: joke.joke.replace(/<([a-zA-Z0-9]+)>/, '<strong>$1</strong>')
          }
        })
        this.medium = json.medium.map(joke => {
          return {
            id: joke.id,
            joke: joke.joke.replace(/<([a-zA-Z0-9]+)>/, '<strong>$1</strong>')
          }
        })
        this.long = json.long.map(joke => {
          return {
            id: joke.id,
            joke: joke.joke.replace(/<([a-zA-Z0-9]+)>/, '<strong>$1</strong>')
          }
        })
      })
    }
  }
}
</script>
