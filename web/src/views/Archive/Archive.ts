import { Component, Vue, Watch } from "vue-property-decorator";
import { ApiObject } from '../../models/apiObject';
import { api } from '@/services/api';


@Component
export default class Archive extends Vue {
    private Volumes = new Array<ApiObject>();
    private Volume = new ApiObject();

    private Issues = new Array<ApiObject>();
    private Issue = new ApiObject();

    private Language = new ApiObject();

    created() {
        this.UpdateLanguage();
        this.intializeData();
    }

    intializeData() {
        api.getVolumeNumbers().then(d => {
            this.Volumes = d;
            if (this.$parent.$data.Volume != undefined) {
                this.Volume = this.$parent.$data.Volume;
            }
            else {
                this.Volume = d[d.length - 1];
                if (this.$parent.$data.Issue != undefined) {
                    this.Issue = this.$parent.$data.Issue;
                }
                else {
                    api.getIssues(parseInt(this.Volume['id'])).then(i => {
                        this.Issue = i[i.length - 1];
                    });
                }
            }
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

    @Watch("$parent.$data.Language")
    UpdateLanguage() {
        this.Language = this.$parent.$data.Language;
    }
}
