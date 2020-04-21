import { Component, Vue, Watch } from "vue-property-decorator";
import { ApiObject } from '../../models/apiObject';
import { api } from '@/services/api';

@Component({
    props: {
        article: {
            type: Object,
            default() { return new Object(); }
        }
    }
})
export default class ArticleThumbnail extends Vue {
    private APP_URL = process.env.VUE_APP_OUTLOOK;

    getCategoryColor(cat: string) {
        return this.$store.state.colors.colors[cat]
    }

    showArticle(article: ApiObject) {
        if (this.$store.getters.Language == undefined || article == undefined) {
            return false;
        }
        return article.language == this.$store.getters.Language.num;
    }
}