import { Component, Vue, Prop, Watch } from "vue-property-decorator";
import { api } from '@/services/api';
import { ApiObject } from '@/models/apiObject';
import TopModel from '../../models/topModel';

@Component({
    props: {
        model: {
            type: TopModel,
            private: true,
            default() { return new TopModel('', '', new Array <ApiObject>(), '', '' ); }
        },
        title: {
            type: String,
            private: true,
            default() { return ''; }
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
        this.Language = this.$parent.$data.Language;
    }
}