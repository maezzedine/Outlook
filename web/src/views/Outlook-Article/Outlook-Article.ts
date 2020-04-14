import { Component, Vue, Watch } from "vue-property-decorator";
import { ApiObject } from '../../models/apiObject';
import { api } from '@/services/api';

@Component
export default class OutlookArticle extends Vue {
    private id: Number | null = null;
    private Article: ApiObject | null = null;
    private Colors: ApiObject | null = null;
    private Language: ApiObject | null = null;
    private APP_URL = process.env.VUE_APP_OUTLOOK;
    private loading = true;

    private body = document.getElementById('article-text-body');

    created() {
        this.getIdFromParams();
        this.getArticle();
        this.getColors();
        this.UpdateLanguage();
        // todo: if the article's language wasn't the app language, switch langauges
    }

    mounted() {
        console.log(this.$el.textContent);
    }


    getColors() {
        var colors = this.$root.$children[0].$data.Colors
        if (colors != undefined) {
            this.Colors = colors;
        }
        else {
            api.getColors().then(d => {
                this.Colors = d;
            });
        }
    }

    getCategoryColor(cat: string) {
        if (this.Colors == null) {
            return "";
        }
        return this.Colors[cat]
    }

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

    @Watch("$parent.$data.Language")
    UpdateLanguage() {
        this.$data.Language = this.$parent.$data.Language;
    }
}
