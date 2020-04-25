import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store/index';
import ArticleHub from './hubs/article-hub';

Vue.config.productionTip = true;

Vue.use(ArticleHub);

new Vue({
    router,
    store,
    render: h => h(App)
}).$mount('#app');
