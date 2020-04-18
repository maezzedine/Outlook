import { Component, Vue, Watch } from "vue-property-decorator";
import { ApiObject } from '@/models/apiObject';
import { api } from '@/services/api';
import articleThumbnail from '@/components/article-thumbnail/Article-Thumbnail';

@Component({
    components: { articleThumbnail },
})
export default class Category extends Vue {
    private Colors: ApiObject | null = null;
    private Category: ApiObject | null = null;
    private Articles: Array<ApiObject> | null = null;
    private id: Number | null = null;

    created() {
        this.getColors();
        this.getIdFromParams();
    }

    getColors() {
        api.getColors().then(d => {
            this.Colors = d;
        });
    }

    getCategoryColor() {
        if (this.Colors == null || this.Category == null) {
            return '';
        }
        return this.Colors[this.Category.tagName]
    }

    getCatgory() {
        if (this.id != null) {
            var categories = this.$parent.$data.Categories;
            if (categories != undefined) {
                for (let c of categories) {
                    if (c.id == this.id) {
                        this.Category = c;
                        return;
                    }
                }
            }
            // If we got this far, then Category wasn't find, so try fetching it from the api
            this.getCategoryFromApi();
        }
        // If we got this far, then Category doesn't exist
        // todo: return to 404
    }

    showCategory() {
        return (this.Category != undefined) && (this.Category.language == this.$store.getters['language/Language'].num);
    }

    @Watch('$route.params.id')
    getIdFromParams() {
        this.id = parseInt(this.$route.params.id);
        this.getCatgory();
    }

    @Watch('$parent.$data.Issue')
    getCategoryFromApi() {
        if (this.id != null && this.Category == null) {
            var issue = this.$parent.$data.Issue;
            if (issue != undefined) {
                api.getCategory(this.id, issue.id).then(d => {
                    this.Category = d;
                    return;
                })
            }
        }
    }

    @Watch('Category')
    @Watch('$parent.$data.Articles')
    updateArticles() {
        if ((this.Category != null) && !(this.$parent.$data.Articles instanceof ApiObject)) {
            var articles = this.$parent.$data.Articles;
            this.Articles = new Array();
            if (articles != undefined) {
                for (let a of articles) {
                    if (a.categoryID == this.Category.id) {
                        this.Articles.push(a);
                    }
                }
            }
        }
    }
}