import { Vue, Component, Watch } from 'vue-property-decorator';
import mainMenu from '@/components/main-menu/MainMenu.vue';
import topStats from '@/components/top-stats/TopStats.vue';
import { api } from '@/services/api';
import { ApiObject } from '@/models/apiObject';
import TopModel from '../../models/topModel';

@Component({
    name: 'Home',
    components: { mainMenu, topStats },
    data() {
        return {
            Volume: undefined,
            Issue: undefined,
            Language: undefined
        }
    }
})
export default class Home extends Vue {
    private TopRatedArticles: TopModel | null = null;
    private TopFavoritedArticles: TopModel | null = null;
    private TopWriters: TopModel | null = null;


    created() {
        this.UpdateIssue();
        this.UpdateVolume();
        this.UpdateLanguage();
        this.getTopArticles();
        this.getTopWriters();
    }

    @Watch('$parent.$data.Issue')
    UpdateIssue() {
        this.$data.Issue = this.$parent.$data.Issue;
    }

    @Watch('$parent.$data.Volume')
    UpdateVolume() {
        this.$data.Volume = this.$parent.$data.Volume;
    }

    volumeToString() {
        var volume = this.$data.Volume;
        if (volume != undefined) {
            return 'Volume ' + volume.volumeNumber + ' | ' + volume.fallYear + ' - ' + volume.springYear;
        }
        return 'No Volume Selected';
    }

    getTopArticles() {
        api.getTopArticles().then(d => {
            this.TopRatedArticles = new TopModel(this.$data.Language.topRatedArticles, 'fas fa-trophy', 'fas fa-thumbs-up', d.topRatedArticles, 'title', 'rate');
            this.TopFavoritedArticles = new TopModel(this.$data.Language.topFavoritedArticles, 'fas fa-medal', 'fas fa-star', d.topFavoritedArticles, 'title', 'numberOfFavorites');
        })
    }

    getTopWriters() {
        api.getTopWriters().then(d => {
            this.TopWriters = new TopModel(this.$data.Language.topWriters, 'fas fa-chart-bar', 'fas fa-file-alt', d, 'name', 'numberOfArticles');
        })
    }

    @Watch("$parent.$data.Language")
    UpdateLanguage() {
        this.$data.Language = this.$parent.$data.Language;
        if (this.TopRatedArticles != null && this.TopFavoritedArticles && this.TopWriters != null) {
            this.TopRatedArticles.setTitle(this.$data.Language.topRatedArticles);
            this.TopFavoritedArticles.setTitle(this.$data.Language.topFavoritedArticles);
            this.TopWriters.setTitle(this.$data.Language.topWriters);
        }
    }
} 