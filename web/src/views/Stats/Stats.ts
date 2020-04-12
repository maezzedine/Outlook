import { Component, Vue, Watch } from "vue-property-decorator";
import topStats from '@/components/top-stats/TopStats.vue';
import TopModel from '../../models/topModel';
import { api } from '@/services/api';

@Component({
    components: { topStats },
    data() {
        return {
            Language: undefined
        }
    }
})
export default class Stats extends Vue {

    private TopRatedArticles: TopModel | null = null;
    private TopFavoritedArticles: TopModel | null = null;
    private TopWriters: TopModel | null = null;

    created() {
        this.UpdateLanguage();
    }

    getTopArticles() {
        api.getTopArticles().then(d => {
            this.TopRatedArticles = new TopModel(this.$data.Language.topRatedArticles, 'fas fa-trophy', 'fas fa-thumbs-up', d.topRatedArticles, 'title', 'rate');
            this.TopFavoritedArticles = new TopModel(this.$data.Language.topFavoritedArticles, 'fas fa-medal', 'fas fa-star', d.topFavoritedArticles, 'title', 'numberOfFavorites');
            this.getTopWriters();
        });
    }

    getTopWriters() {
        api.getTopWriters().then(d => {
            this.TopWriters = new TopModel(this.$data.Language.topWriters, 'fas fa-chart-bar', 'fas fa-file-alt', d, 'name', 'numberOfArticles');

            if (this.TopRatedArticles != null && this.TopFavoritedArticles && this.TopWriters != null) {
                this.$data.Models = new Array<TopModel>(this.TopRatedArticles, this.TopFavoritedArticles, this.TopWriters);
            }
        })
    }

    @Watch("$parent.$parent.$data.Language")
    UpdateLanguage() {
        this.$data.Language = this.$parent.$parent.$data.Language;
        if (this.$data.Language != undefined) {
            this.getTopArticles();
        }
    }
}