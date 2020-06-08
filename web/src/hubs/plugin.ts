import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr';
import vue, { PluginObject } from 'vue';
import ArticleHub from './Models/articleHub';

//const API_URL = process.env.VUE_APP_API_OUTLOOK;


const install = (Vue: typeof vue): void => {
    // use new Vue instance as an event bus
    const articleHub = new ArticleHub(Vue);
    // every component will use this.$articleHub to access the event bus
    Vue.prototype.$articleHub = articleHub;

    Object.defineProperties(Vue.prototype, {
        $articleHub: {
            get() {
                return articleHub;
            }
        }
    })

    //const connection = new HubConnectionBuilder()
    //    .withUrl(`${API_URL}/article-hub`)
    //    .configureLogging(LogLevel.Information)
    //    .build();

    //// Forward server side SignalR events through $articleHub, where components will listen to them
    //connection.on('ArticleScoreChange', (articleId, rate, numberOfVotes) => {
    //    articleHub.$emit('article-score-changed', { articleId, rate, numberOfVotes })
    //})

    //connection.on('ArticleCommentChange', (articleId, comments) => {
    //    articleHub.$emit('article-comment-changed', { articleId, comments })
    //})

    //connection.on('ArticleFavoriteChange', (articleId, numberOfFavorites) => {
    //    articleHub.$emit('article-favorite-changed', { articleId, numberOfFavorites })
    //})

    //let startedPromise = null;
    //function start() {
    //    startedPromise = connection.start().catch(err => {
    //        console.error('Failed to connect with hub', err);
    //        return new Promise((resolve, reject) =>
    //            setTimeout(() => start().then(resolve as any).catch(reject), 5000))
    //    })
    //    return startedPromise;
    //}

    //connection.onclose(() => start());

    //start();
}

const plugin: PluginObject<any> = {
    install
}

export default plugin;