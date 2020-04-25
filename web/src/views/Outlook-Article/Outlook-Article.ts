import { Component, Vue, Watch } from "vue-property-decorator";
import { ApiObject } from '../../models/apiObject';
import { api, Api } from '@/services/api';

@Component
export default class OutlookArticle extends Vue {
    private APP_URL = process.env.VUE_APP_OUTLOOK;
    private loading = true;

    private id: Number | null = null;
    private Article: ApiObject | null = null;

    private Comment: string | null = null;

    created() {
        this.getIdFromParams();
        this.getArticle();
        // todo: if the article's language wasn't the app language, switch langauges
    }

    getCategoryColor(cat: string) {
        return this.$store.state.colors.colors[cat]
    }

    @Watch('$parent.$data.Articles')
    getArticle() {
        if (this.id != null) {
            var articles = this.$parent.$data.Articles;
            if (articles != undefined) {
                for (let a of articles) {
                    if (a.id == this.id) {
                        this.Article = a;
                        this.loading = false;
                        return;
                    }
                }
            }
            // If we got this far, then Category wasn't find, so try fetching it from the api
            this.getArticlesFromApi();
        }
        // If we got this far, then Category doesn't exist
        // todo: return to 404
    }

    getArticlesFromApi() {
        if (this.id != null && this.Article == null) {
            api.getArticle(this.id).then(d => {
                this.Article = d;
                this.loading = false;
                return;
            })
        }
    }

    rateUp() {
        if (this.Article != null && this.$store.getters.IsAuthenticated) {
            api.rateUpArticle(this.$store.getters.User.token, this.Article.id);
        }
    }

    rateDown() {
        if (this.Article != null && this.$store.getters.IsAuthenticated) {
            api.rateDownArticle(this.$store.getters.User.token, this.Article.id);
        }
    }

    addComment() {
        if (this.Article != null && this.$store.getters.IsAuthenticated && this.Comment != null) {
            api.addComment(this.$store.getters.User.token, this.Article.id, this.Comment).then(r => this.Comment = '');
        }
    }

    getDateTime(datetime: string) {
        var date = new Date(datetime);
        return date.toDateString() + ' - ' + date.toLocaleTimeString();
    }

    favoriteArticle() {
        if (this.Article != null && this.$store.getters.IsAuthenticated) {
            api.favoriteArticle(this.$store.getters.User.token, this.Article.id);
        }
    }

    @Watch("Article")
    fillBodyText() {
        var body = document.getElementById('article-text-body');
        if (body != null && this.Article != null) {
            body.innerHTML = this.Article.text;
        }
    }

    @Watch('$route.params.id')
    getIdFromParams() {
        this.id = parseInt(this.$route.params.id);
    }
}
