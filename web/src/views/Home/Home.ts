import { Vue, Component, Watch } from 'vue-property-decorator';
import mainMenu from '@/components/main-menu/MainMenu.vue';
import topArticles from '@/components/top-articles/TopArticles.vue';
import { api } from '@/services/api';
import { ApiObject } from '@/models/apiObject';

@Component({
    name: 'Home',
    components: { mainMenu, topArticles },
    data() {
        return {
            Volume: undefined,
            Issue: undefined,
            TopRatedArticles: undefined,
            TopFavoritedArticles: undefined,
            Language: undefined
        }
    }
})
export default class Home extends Vue {
    created() {
        this.UpdateIssue();
        this.UpdateVolume();
        this.getTopArticles();
        this.UpdateLanguage();
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
            this.$data.TopRatedArticles = d.topRatedArticles;
            this.$data.TopFavoritedArticles = d.topFavoritedArticles;
        })
    }

    @Watch("$parent.$data.Language")
    UpdateLanguage() {
        this.$data.Language = this.$parent.$data.Language;
    }
} 