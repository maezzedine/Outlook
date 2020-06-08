import Vue, { VueConstructor } from 'vue';
import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr';

const API_URL = process.env.VUE_APP_API_OUTLOOK;

export default class ArticleHub extends Vue {
    public number = 0;

    constructor(Vue: VueConstructor) {
        super();
        const connection = new HubConnectionBuilder()
            .withUrl(`${API_URL}/article-hub`)
            .configureLogging(LogLevel.Information)
            .build();

        //const component = new Vue();

        // Forward server side SignalR events through $articleHub, where components will listen to them
        connection.on('ArticleScoreChange', (articleId, rate, numberOfVotes) => {
            this.$emit('article-score-changed', { articleId, rate, numberOfVotes })
        })

        connection.on('ArticleCommentChange', (articleId, comments) => {
            this.$emit('article-comment-changed', { articleId, comments })
        })

        connection.on('ArticleFavoriteChange', (articleId, numberOfFavorites) => {
            this.$emit('article-favorite-changed', { articleId, numberOfFavorites })
        })

        let startedPromise = null;
        function start() {
            startedPromise = connection.start().catch(err => {
                if (process.env.NODE_ENV != 'production') console.error('Failed to connect with hub', err);
                return new Promise((resolve, reject) =>
                    setTimeout(() => start().then(resolve as any).catch(reject), 5000))
            })
            return startedPromise;
        }

        connection.onclose(() => start());

        start();
    }
}