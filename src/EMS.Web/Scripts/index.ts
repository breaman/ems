import 'es6-promise/auto';
import Vue from 'vue';
import store from './store';
import Teams from './team/teams.vue';

import 'bootstrap';

import '../Styles/main.scss';

new Vue({
    el: '#app',
    template: '<Teams/>',
    store,
    components: {
        Teams
    }
});