import Vue from 'vue';
import App from './app.vue';

let v = new Vue({
    el: '#app',
    template: '<App description="sweet, so this is working." />',
    components: {
        App
    }
})