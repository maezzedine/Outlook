import { Component, Vue, Watch } from "vue-property-decorator";
import { ApiObject } from '../../models/apiObject';
import { api } from '../../services/api';

@Component
export default class OutlookWriters extends Vue {
    private Writers: Array<ApiObject> | null = null;

    created() {
        this.getWriters();
    }

    getWriters() {
        api.getWriters().then(d => {
            this.Writers = d;
        });
    }
}