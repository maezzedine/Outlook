import { Component, Vue, Watch } from "vue-property-decorator";
import { ApiObject } from '../../models/apiObject';
import { api } from '@/services/api';


@Component
export default class Archive extends Vue {
    private Volumes = new Array<ApiObject>();
    private Volume = new ApiObject();

    private Issues = new Array<ApiObject>();
    private Issue = new ApiObject();

    created() {
        this.Volume = this.$parent.$data.Volume;
        this.Issue = this.$parent.$data.Issue;

        api.getVolumeNumbers().then(d => {
            this.Volumes = d;
        });
    }

    @Watch("Issue")
    updateIssue() {
        this.Issue = this.Issue;
        this.$emit('set-issue', this.Issue);
    }

    @Watch("Volume")
    getIssues() {
        this.$emit('set-volume', this.Volume);
        api.getIssues(parseInt(this.Volume['id'])).then(i => {
            this.Issues = i;
            this.Issue = i[i.length - 1];
        });
    }
}
