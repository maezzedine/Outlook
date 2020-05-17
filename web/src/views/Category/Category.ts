import { Component, Vue, Watch } from "vue-property-decorator";
import { ApiObject } from '@/models/apiObject';
import { api } from '@/services/api';
import articleThumbnail from '@/components/article-thumbnail/Article-Thumbnail';

@Component({
    components: { articleThumbnail },
})
export default class Category extends Vue {
    private Category: ApiObject | null = null;
    private id: Number | null = null;

    created() {
        this.getIdFromParams();
    }

    getCategoryColor() {
        if (this.Category == null) {
            return '';
        }
        return this.$store.state.colors.colors[this.Category.tag]
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
    }

    showCategory() {
        return (this.Category != undefined) && (this.Category.language == this.$store.getters.Language.language);
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
                var params = new Array<Number>();
                params.push(this.id, issue.id);
                api.Get('categories', params)
                    .then(d => {
                        this.Category = d;
                        return;
                    })
                    .catch(_ => {
                        this.$router.push(`/${this.$store.getters.Language.lang}/not-found`);
                    })
            }
        }
    }
}