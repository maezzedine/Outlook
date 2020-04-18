import Vue from 'vue';
import Router from 'vue-router';

Vue.use(Router);

const Home = () => import('./views/Home/Home.vue');
const Archives = () => import('./views/Archive/Archive.vue');
const Category = () => import('./views/Category/Category.vue');
const Writers = () => import('./views/Outlook-Writers/Outlook-Writers.vue');
const Article = () => import('./views/Outlook-Article/Outlook-Article.vue');

//function prefixRoutes(prefix: string, routes: RouteConfig) {
//    return routes.map(route => route.path = prefix + '/' + route.path)
//}

export default new Router({
    mode: 'history',
    routes: [
        //...prefixRoutes('/:lang', [
            {
                name: 'home',
                path: '/:lang',
                component: Home,
            },
            {
                name: 'archives',
                path: '/:lang/archives',
                component: Archives
            },
            {
                name: 'category',
                path: '/:lang/category/:id',
                component: Category
            },
            {
                name: 'writers',
                path: '/:lang/writers',
                component: Writers
            },
            {
                name: 'article',
                path: '/:lang/article/:id',
                component: Article
            }
        //])
    ]
});