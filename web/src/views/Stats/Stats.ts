import { Component, Vue, Watch } from "vue-property-decorator";
import topStats from '@/components/top-stats/TopStats.vue';
import TopModel from '../../models/topModel';
import { api } from '@/services/api';

@Component({
    components: { topStats },
})
export default class Stats extends Vue {

    private TopRatedArticles: TopModel | null = null;
    private TopFavoritedArticles: TopModel | null = null;
    private TopWriters: TopModel | null = null;

    created() {
        this.getTopArticles();
    }

    @Watch('$store.getters.Language')
    getTopArticles() {
        var topArticlesFromSession = sessionStorage.getItem('outlook-top-articles');
        if (topArticlesFromSession != null) {
            var topArticles = JSON.parse(topArticlesFromSession);
            this.assignTopArticles(topArticles);
        }
        else {
            api.Get('articles').then(d => {
                sessionStorage.setItem('outlook-top-articles', JSON.stringify(d));
                this.assignTopArticles(d);
            });
        }
        this.getTopWriters();
    }

    assignTopArticles(d: any) {
        this.TopRatedArticles = new TopModel(this.$store.getters.Language.topRatedArticles, 'trophy', 'rate', d.topRatedArticles, 'title', 'rate', 'article');
        this.TopFavoritedArticles = new TopModel(this.$store.getters.Language.topFavoritedArticles, 'medal', 'star', d.topFavoritedArticles, 'title', 'numberOfFavorites', 'article');
    }

    getTopWriters() {
        var topwritersFromSession = sessionStorage.getItem('outlook-top-writers');
        if (topwritersFromSession != null) {
            var topWriters = JSON.parse(topwritersFromSession);
            this.assignTopWriters(topWriters);
        }
        else {
            api.Get('members/top').then(d => {
                sessionStorage.setItem('outlook-top-writers', JSON.stringify(d));
                this.assignTopWriters(d);
            });
        }
    }

    assignTopWriters(d: any) {
        this.TopWriters = new TopModel(this.$store.getters.Language.topWriters, 'chart', 'file', d, 'name', 'numberOfArticles', 'member');

        if (this.TopRatedArticles != null && this.TopFavoritedArticles && this.TopWriters != null) {
            this.$data.Models = new Array<TopModel>(this.TopRatedArticles, this.TopFavoritedArticles, this.TopWriters);
        }
    }

}