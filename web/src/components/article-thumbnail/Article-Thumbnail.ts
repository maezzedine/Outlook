import { Component, Vue, Watch } from "vue-property-decorator";
import { ApiObject } from '../../models/apiObject';
import svgArrowUp from '@/components/svgs/svg-arrow-up.vue';
import svgArrowDown from '@/components/svgs/svg-arrow-down.vue';
import svgStarEmpty from '@/components/svgs/svg-star-empty.vue';
import svgStarFill from '@/components/svgs/svg-star-fill.vue';

@Component({
    components: { svgArrowUp, svgArrowDown, svgStarEmpty, svgStarFill },
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