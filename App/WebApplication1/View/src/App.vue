<template>
    <div class="container">
        <nav class="navbar navbar-expand-lg navbar-light bg-light">
            <a class="navbar-brand" href="#">My Site</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav">
                    <li class="nav-item active">
                        <router-link class="nav-link" to="/">Home</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link class="nav-link" to="/about">About</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link class="nav-link" to="/register">Register</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link class="nav-link" to="/trainMain">TrainSystem</router-link>
                    </li>
                </ul>
            </div>
        </nav>
        <!--<img class="img-fluid" alt="Vue logo" src="./assets/logo.png">
    <h1 class="display-1 text-bold"> My App </h1>
    <h1 class="text-gray-soft text-regular"> Greetings to you all. </h1>
    <router-link to="/">Home</router-link>
    <router-link to="/about">About</router-link>
    <router-link to="/register">Register</router-link>
    <router-link to="/trainMain">TrainSystem</router-link>-->
        <b-container fluid>
            <router-view></router-view>
        </b-container>
    </div>
</template>

<script>

export default {
  name: 'App'
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
