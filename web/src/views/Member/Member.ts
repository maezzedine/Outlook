import { Component, Vue, Watch } from "vue-property-decorator";
import { ApiObject } from '../../models/apiObject';
import { api } from '../../services/api';
import articleThumbnail from '@/components/article-thumbnail/Article-Thumbnail.vue';

@Component({
    components: { articleThumbnail }
})
export default class Member extends Vue {
    private member = new ApiObject();

    created() {
        this.getMember();
    }

    @Watch('$route.params.id')
    getMember() {
        var id = this.$route.params.id;
        if (id != null) {
            api.getMember(id).then(d => {
                this.member = d;
            })
        }
    }
}