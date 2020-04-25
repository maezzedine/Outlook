import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr';
import vue from 'vue';

const APP_URL = process.env.VUE_APP_OUTLOOK;

export default {
    install(Vue: typeof vue) {
        // use new Vue instance as an event bus
        const articleHub = new Vue();
        // every component will use this.$articleHub to access the event bus
        Vue.prototype.$articleHub = articleHub;

        const connection = new HubConnectionBuilder()
            .withUrl(`${APP_URL}/article-hub`)
            .configureLogging(LogLevel.Information)
            .build();

        // Forward server side SignalR events through $articleHub, where components will listen to them
        connection.on('ArticleScoreChange', (articleId, rate, numberOfVotes) => {
            articleHub.$emit('article-score-changed', { articleId, rate, numberOfVotes })
        })

        connection.on('ArticleCommentChange', (articleId, comments) => {
            articleHub.$emit('article-comment-changed', { articleId, comments })
        })

        connection.on('ArticleFavoriteChange', (articleId, numberOfFavorites) => {
            articleHub.$emit('article-favorite-changed', { articleId, numberOfFavorites })
        })

        let startedPromise = null;
        function start() {
            startedPromise = connection.start().catch(err => {
                console.error('Failed to connect with hub', err);
                return new Promise((resolve, reject) =>
                    setTimeout(() => start().then(resolve).catch(reject), 5000))
            })
            return startedPromise;
        }

        connection.onclose(() => start());

        start();
    }
}
