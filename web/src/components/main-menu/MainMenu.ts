import { Component, Vue, Watch } from "vue-property-decorator";
import { ApiObject } from '../../models/apiObject';
import articleThumbnail from '@/components/article-thumbnail/Article-Thumbnail.vue';


@Component({
    components: { articleThumbnail }
})
export default class MainMenu extends Vue {
    private Articles = new Array<ApiObject>();
    private Language = new ApiObject();

    created() {
        this.updateArticles();
        this.UpdateLanguage();
    }


    @Watch('$parent.$parent.$data.Articles')
    updateArticles() {
        this.Articles = this.$parent.$parent.$data.Articles;
    }

    @Watch("$parent.$parent.$data.Language")
    UpdateLanguage() {
        this.Language = this.$parent.$parent.$data.Language;
    }
}