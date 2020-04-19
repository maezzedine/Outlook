import Vue from 'vue';
import Router, { RouteConfig } from 'vue-router';

Vue.use(Router);

const Home = () => import('./views/Home/Home.vue');
const Archives = () => import('./views/Archive/Archive.vue');
const Category = () => import('./views/Category/Category.vue');
const Writers = () => import('./views/Outlook-Writers/Outlook-Writers.vue');
const Article = () => import('./views/Outlook-Article/Outlook-Article.vue');
const MeetOutlook = () => import('./views/Meet-Outlook/Meet-Outlook.vue');

const withPrefix = (prefix: string, routes: Array<RouteConfig>) =>
    routes.map((route) => {
        route.path = prefix + route.path;
        return route;
    });

export default new Router({
    mode: 'history',
    routes: [
        ...withPrefix('/:lang', [
            {
                name: 'home',
                path: '',
                component: Home,
            },
            {
                name: 'archives',
                path: '/archives',
                component: Archives
            },
            {
                name: 'category',
                path: '/category/:id',
                component: Category
            },
            {
                name: 'writers',
                path: '/writers',
                component: Writers
            },
            {
                name: 'article',
                path: '/article/:id',
                component: Article
            },
            {
                name: 'about',
                path: '/meet-outlook',
                component: MeetOutlook
            }
        ])
    ]
});