import { createRouter, createWebHistory } from 'vue-router'
import RonHome from '../components/Home.vue'
import RonAbout from '../components/About.vue'
import RonRegister from '../components/Register.vue'

const routes = [
    {
        path: '/',
        name: 'Home',
        component: RonHome
    },
    {
        path: '/about',
        name: 'About',
        component: RonAbout
    },
    {
        path: '/register',
        name: 'Register',
        component: RonRegister
    }
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
})

export default router