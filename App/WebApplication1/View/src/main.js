import { createApp } from 'vue'
import App from './App.vue'

import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap"
import router from './router'

function getData(url) {
    url = `https://localhost:7135/${url}`;
    console.log(url);
    return new Promise((resolve, reject) => {
        fetch(url)
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(data => {
                resolve(data);
            })
            .catch(error => {
                reject(error);
            });
    });
}
var app = createApp(App);
app.config.globalProperties.$getData = getData;
app.use(router).mount('#app');
