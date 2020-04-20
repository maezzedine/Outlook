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

    getTopArticles() {
        api.getTopArticles().then(d => {
            this.TopRatedArticles = new TopModel(this.$store.getters.Language.topRatedArticles, 'fas fa-trophy', 'fas fa-thumbs-up', d.topRatedArticles, 'title', 'rate', 'article');
            this.TopFavoritedArticles = new TopModel(this.$store.getters.Language.topFavoritedArticles, 'fas fa-medal', 'fas fa-star', d.topFavoritedArticles, 'title', 'numberOfFavorites', 'article');
            this.getTopWriters();
        });
    }

    getTopWriters() {
        api.getTopWriters().then(d => {
            this.TopWriters = new TopModel(this.$store.getters.Language.topWriters, 'fas fa-chart-bar', 'fas fa-file-alt', d, 'name', 'numberOfArticles', 'writer');

            if (this.TopRatedArticles != null && this.TopFavoritedArticles && this.TopWriters != null) {
                this.$data.Models = new Array<TopModel>(this.TopRatedArticles, this.TopFavoritedArticles, this.TopWriters);
            }
        })
    }

}