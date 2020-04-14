import Vue from 'vue';
import Router from 'vue-router';

Vue.use(Router);

const Home = () => import('./views/Home/Home.vue');
const Archives = () => import('./views/Archive/Archive.vue');
const Category = () => import('./views/Category/Category.vue');
const Writers = () => import('./views/Outlook-Writers/Outlook-Writers.vue');

export default new Router({
    mode: 'history',
    routes: [
        {
            path: '*',
            redirect: '/'
        },
        {
            name: 'home',
            path: '/',
            component: Home
        },
        {
            name: 'archives',
            path: '/archives',
            component: Archives
        },
        {
            name: 'category',
            path: '/category/:id?',
            component: Category
        },
        {
            name: 'writers',
            path: '/writers',
            component: Writers
        },
    ]
});