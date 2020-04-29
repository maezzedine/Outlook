import Vue from 'vue';
import Router, { RouteConfig, Route } from 'vue-router';
import store from './store';

Vue.use(Router);

const Home = () => import('./views/Home/Home.vue');
const Archives = () => import('./views/Archive/Archive.vue');
const Category = () => import('./views/Category/Category.vue');
const Writers = () => import('./views/Outlook-Writers/Outlook-Writers.vue');
const Member = () => import('./views/Member/Member');
const Article = () => import('./views/Outlook-Article/Outlook-Article.vue');
const MeetOutlook = () => import('./views/Meet-Outlook/Meet-Outlook.vue');
const UploadArticle = () => import('./views/Upload-Article/Upload-Article.vue');
const About = () => import('./views/About/About.vue');
const Login = () => import('./views/Authentication/Login.vue');
const Register = () => import('./views/Authentication/Register.vue');

const withPrefix = (prefix: string, routes: Array<RouteConfig>) =>
    routes.map((route) => {
        route.path = prefix + route.path;
        return route;
    });

const isAuthenticated = (to: Route, from: Route, next: Function) => {
    if (to.meta.authPage && store.getters.IsAuthenticated) {
        return;
    }
    next();
}

export default new Router({
    mode: 'history',
    routes: [
        ...withPrefix('/:lang', [
            {
                name: 'home',
                path: '/',
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
                name: 'meet-outlook',
                path: '/meet-outlook',
                component: MeetOutlook
            },
            {
                name: 'member',
                path: '/member/:id',
                component: Member
            },
            {
                name: 'login',
                path: '/login',
                component: Login,
                meta: { authPage: true },
                beforeEnter: isAuthenticated,
            },
            {
                name: 'register',
                path: '/register',
                component: Register,
                meta: { authPage: true },
                beforeEnter: isAuthenticated
            },
            {
                name: 'upload-article',
                path: '/upload-article',
                component: UploadArticle
            },
            {
                name: 'about',
                path: '/about',
                component: About
            }
        ])
    ]
});