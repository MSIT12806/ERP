<template>
    <div>
        <h1>Home Page</h1>
        <p>Welcome to the Home page.</p>
        <HelloWorld msg="Welcome to Your Vue.js App" />
    </div>
</template>

<script>
    import HelloWorld from '@/components/HelloWorld.vue'
    export default {
        name: 'RonHome',
        components: {
            HelloWorld
        }
    }
</script>