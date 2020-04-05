import Vue from 'vue';
import Router from 'vue-router';

Vue.use(Router);

import Home from './views/Home/Home.vue';
import Archives from './views/Archive/Archive.vue';

export default new Router({
    mode: 'history',
    routes: [
        {
            path: "/",
            name: 'Home',
            component: Home
        },
        {
            path: "/Archives",
            name: 'Archives',
            component: Archives
        },
    ]
});