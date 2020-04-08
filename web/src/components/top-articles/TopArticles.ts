import { Component, Vue, Prop, Watch } from "vue-property-decorator";
import { api } from '@/services/api';
import { ApiObject } from '@/models/apiObject';

@Component({
    props: {
        title: {
            type: String,
            private: true,
            default() { return ''; }
        },
        articles: {
            type: Array,
            private: true,
            default() { return new Array<ApiObject>(); }
        }

    }
})
export default class TopArticles extends Vue {
    private Language = new ApiObject();

    cteated() {
        this.UpdateLanguage();
    }

    @Watch("$parent.$data.Language")
    UpdateLanguage() {
        this.Language = this.$parent.$parent.$data.Language;
    }
}