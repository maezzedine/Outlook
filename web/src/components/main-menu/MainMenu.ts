import { Component, Vue, Watch } from "vue-property-decorator";
import { ApiObject } from '../../models/apiObject';
import { api } from '@/services/api';


@Component
export default class MainMenu extends Vue {
    private Articles = new Array<ApiObject>();
    private APP_URL = process.env.VUE_APP_OUTLOOK;
    private Language = new ApiObject();
    private Colors: ApiObject | null = null;

    created() {
        this.getColors();
        this.updateArticles();
        this.UpdateLanguage();
    }

    showArticle(article: ApiObject) {
        if (this.Language == undefined || article == undefined) {
            return false;
        }
        return article.lang == this.Language.lang;
    }

    async getColors() {
        this.Colors = await api.getColors();
    }

    getCategoryColor(cat: string) {
        if (this.Colors == null) {
            return "";
        }
        return this.Colors[cat]
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