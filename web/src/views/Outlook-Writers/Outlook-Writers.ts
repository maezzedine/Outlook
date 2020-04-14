import { Component, Vue, Watch } from "vue-property-decorator";
import { ApiObject } from '../../models/apiObject';
import { api } from '../../services/api';

@Component
export default class OutlookWriters extends Vue {
    private Writers: Array<ApiObject> | null = null;
    private Language: ApiObject | null = null;

    created() {
        this.getWriters();
        this.UpdateLanguage();
    }

    getWriters() {
        api.getWriters().then(d => {
            this.Writers = d;
        });
    }

    @Watch("$parent.$data.Language")
    UpdateLanguage() {
        this.$data.Language = this.$parent.$data.Language;
    }
}