import ArticleHub from './Models/articleHub';
import plugin from './plugin';

declare module 'vue/types/vue' {
    interface Vue {
        $articleHub: ArticleHub;
    }

    interface VueConstructor {
        articleHub: ArticleHub;
    }
}

export default plugin;