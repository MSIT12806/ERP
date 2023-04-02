<template>
    <div class="container">
        <img class="img-fluid" alt="Vue logo" src="./assets/logo.png">
        <h1 class="display-1 text-bold"> My App </h1>
        <h1 class="text-gray-soft text-regular"> Greetings to you all. </h1>
        <HelloWorld msg="Welcome to Your Vue.js App" />
    </div>
</template>

<script>
import HelloWorld from './components/HelloWorld.vue'

export default {
  name: 'App',
  components: {
    HelloWorld
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
