import { Component, Vue, Watch } from "vue-property-decorator";
import { ApiObject } from '../../models/apiObject';
import { api } from '@/services/api';
import { cacher } from '@/services/cacher';


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
        api.getVolumeNumbers().then(v => {
            this.Volumes = v;
            this.Volume = (this.$parent.$data.Volume != undefined) ? this.$parent.$data.Volume : cacher.initializeVolume(v);
            
            api.getIssues(parseInt(this.Volume['id'])).then(i => {
                this.Issues = i;
                this.Issue = (this.$parent.$data.Issue != undefined) ? this.$parent.$data.Issue : cacher.initializeIssue(i);
            });
        });
    }

    @Watch("Issue")
    updateIssue() {
        if (this.$parent.$data.Issue != this.Issue) {
            this.$emit('set-issue', this.Issue);
        }
    }

    @Watch("Volume")
    getIssues() {
        if (this.$parent.$data.Volume != this.Volume) {
            this.$emit('set-volume', this.Volume);
        }
        if (!(this.Issue instanceof ApiObject)) {
            // this condition won't be satisfied when reloading the page
            api.getIssues(parseInt(this.Volume['id'])).then(i => {
                this.Issues = i;
                this.Issue = i[i.length - 1];
            });
        }
    }

    @Watch("$parent.$data.Language")
    UpdateLanguage() {
        this.Language = this.$parent.$data.Language;
    }
}
