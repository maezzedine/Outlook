import { Component, Vue, Watch } from "vue-property-decorator";
import { ApiObject } from '../../models/apiObject';
import { api } from '@/services/api';
import svgArrowUp from '@/components/svgs/svg-arrow-up.vue';
import svgArrowDown from '@/components/svgs/svg-arrow-down.vue';
import svgStarEmpty from '@/components/svgs/svg-star-empty.vue';
import svgStarFill from '@/components/svgs/svg-star-fill.vue';
import svgHeart from '@/components/svgs/svg-heart.vue';
import svgClose from '@/components/svgs/svg-close.vue';
import { start } from 'repl';
import { setTimeout } from 'timers';

@Component({
    components: { svgArrowUp, svgArrowDown, svgStarEmpty, svgStarFill, svgHeart, svgClose }
})
export default class OutlookArticle extends Vue {
    private APP_URL = process.env.VUE_APP_OUTLOOK;
    private loading = true;

    private id: Number | null = null;

    private Comment: string | null = null;

    created() {
        this.getIdFromParams();
        this.getArticle();
        this.fillBodyText();
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
                        this.loading = false;
                        this.$store.dispatch('setArticle', a);
                        return;
                    }
                }
            }
            // If we got this far, then Category wasn't find, so try fetching it from the api
            this.getArticlesFromApi();
        }
    }

    getArticlesFromApi() {
        if (this.id != null) {
            var params = new Array<Number>();
            params.push(this.id)
            api.Get('articles/article', params)
                .then(d => {
                    this.loading = false;
                    this.$store.dispatch('setArticle', d);
                    return;
                })
                .catch(e => {
                    this.$router.push(`/${this.$store.getters.Language.lang}/not-found`);
                })
        }
    }

    rateUp() {
        if (this.$store.getters.Article != null && this.$store.getters.IsAuthenticated) {
            var params = new Array<string>();
            params.push(this.$store.getters.Article.id);
            api.AuthorizedAction('PUT', 'articles/RateUpArticle', params);
        }
    }

    rateDown() {
        if (this.$store.getters != null && this.$store.getters.IsAuthenticated) {
            var params = new Array<string>();
            params.push(this.$store.getters.Article.id);
            api.AuthorizedAction('PUT', 'articles/RateDownArticle', params);
        }
    }

    addComment() {
        if (this.$store.getters.Article != null && this.$store.getters.IsAuthenticated && this.Comment != null) {
            var raw = JSON.stringify({ "ArticleId": this.$store.getters.Article.id, "Text": this.Comment });
            api.AuthorizedAction('POST', 'comments', undefined, raw);
            this.Comment = null;
        }
    }

    deleteComment(commentID: Number) {
        var params = new Array<Number>();
        params.push(commentID);
        api.AuthorizedAction('DELETE', 'comments', params);
    }

    getDateTime(datetime: string) {
        var date = new Date(datetime);
        return date.toDateString() + ' - ' + date.toLocaleTimeString();
    }

    favoriteArticle() {
        if (this.$store.getters.Article != null && this.$store.getters.IsAuthenticated) {
            var params = new Array<string>();
            params.push(this.$store.getters.Article.id);
            api.AuthorizedAction('PUT', 'articles/FavoriteArticle', params);
        }
    }

    @Watch("$store.getters.Article")
    fillBodyText() {
        var body = document.getElementById('article-text-body');
        if (body != null && this.$store.getters.Article != null) {
            body.innerHTML = this.$store.getters.Article.text;
        }
        else {
            // if page was not yet loaded, try again after 1 sec
            new Promise(resolve => setTimeout(resolve, 1000)).then(_ => this.fillBodyText());
        }
    }

    @Watch('$route.params.id')
    getIdFromParams() {
        this.id = parseInt(this.$route.params.id);
    }
}
