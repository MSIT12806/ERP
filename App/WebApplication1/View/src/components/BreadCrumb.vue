<template>
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
        <a class="navbar-brand" href="#">My Site</a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNav">
            <ul class="navbar-nav">
                <li class="nav-item" v-for="link in links" :key="link.id" >
                    <router-link class="nav-link" :class="{ 'active': link.url === currentPath }" :to="link.url">{{ link.text }}</router-link>
                </li>
                <!--<li class="nav-item active">
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
                </li>-->
            </ul>
        </div>
    </nav>
</template>

<script>

    export default {
        name: 'BreadCrumb',
        props: {
            links: {
                type: Array,
                required: true
            },
            currentPath: {
                type: String,
                required: true
            },
        },
        watch: {
            $route(newVal, oldVal) {
                // 在路由切換時更新links的值
                // 你可以從新路由的meta中取得對應的links數組，並且更新到父層元件中的links中
                console.log(newVal);
                console.log(oldVal);
                this.$emit('update:links', newVal.meta.links);
            }
        },
    }
</script>
