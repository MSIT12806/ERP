<template>
    <div class="container">
        <!--<bread-crumb :links="links" :currentPath="currentPath" @update:links="updateLinks" />
        <img class="img-fluid" alt="Vue logo" src="./assets/logo.png">
        <h1 class="display-1 text-bold"> My App </h1>
        <h1 class="text-gray-soft text-regular"> Greetings to you all. </h1>-->
        <router-link to="/">Home</router-link>
        <router-link to="/about">About</router-link>
        <router-link to="/register">Register</router-link>
        <router-link to="/trainMain">TrainSystem</router-link>
        <b-container fluid>
            <router-view></router-view>
        </b-container>
    </div>
</template>

<script>
    import BreadCrumb from './components/BreadCrumb.vue'

    export default {
        name: 'App',
        components: {
            BreadCrumb,
        },
        data() {
            return {
                links: [
                    { text: "Home", url: "/" },
                    { text: "About", url: "/about" },
                ],
                currentPath: "",
                routeTree: {
                    children:
                        [
                            {
                                text: "Home", url: "/",
                                children: [
                                    { text: "About", url: "/about", children: [] },
                                    { text: "Register", url: "/register", children: [] },
                                    { text: "TrainSystem", url: "/trainMain", children: [] },

                                ]
                            },
                        ]
                },
            }
        },
        methods: {
            updateLinks(links) {
                console.log(links);
                this.links = links; // 更新links數組
                this.currentPath = "hihi";
            },
            getRoutePath(nodeName) {
                var r = [];
                var stack = [];
                stack.push(...this.routeTree.children);
                var currentNode = stack.pop();
                while (true) {
                    r.push(currentNode.text);
                    if (nodeName == currentNode.Text) break;
                    if (Array.isArray(currentNode.children) && currentNode.children.length > 0) {
                        currentNode.checked = true;
                        stack.push(currentNode);
                        stack.push(...currentNode.children);
                    }
                    else {
                        r.pop();
                    }
                    currentNode = stack.pop();
                    if (currentNode.checked === true) {
                        stack.pop();
                        r.pop();
                    }
                    if (stack.length === 0) break;
                }

                return r;
            },

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
