import { Component, Vue } from "vue-property-decorator";
import { api } from '../../../services/api';
import articleThumbnail from '../../../components/article-thumbnail/Article-Thumbnail.vue';

@Component({
    components: { articleThumbnail }
})
export default class FavoritedArticles extends Vue {
    private Articles = new Array();

    created() {
        this.getFavoritedArticles();
    }

    getFavoritedArticles() {
        api.favoritedArticles().then(d => {
            this.Articles = d;
        })
    }
}