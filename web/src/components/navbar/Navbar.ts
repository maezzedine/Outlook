import { Vue, Component, Prop } from 'vue-property-decorator';
import svgOutlook from '@/components/svgs/svg-outlook.vue';
import { api } from '@/services/api';
import { ApiObject } from '../../models/apiObject';

@Component({
    components: { svgOutlook },
})
export default class Navbar extends Vue {
    private Volumes = new Array<ApiObject>();
    private Volume!: ApiObject;

    private Issues = new Array<ApiObject>();
    private Issue!: ApiObject;

    @Prop() language!: ApiObject;
    
    created() {
        api.getVolumeNumbers().then(d => {
            this.Volumes = d;
            this.setVolume(d[d.length - 1]);
        });
    }

    setVolume(volume: ApiObject) {
        if (this.Volume != volume) {
            this.Volume = volume;
            this.$emit('set-volume', volume);
            this.getIssues(parseInt(volume['id']));
        }
    }

    setIssue(issue: ApiObject) {
        if (this.Issue != issue) {
            this.Issue = issue;
            this.$emit('set-issue', issue);
        }
    }

    getIssues(volumeId: Number) {
        api.getIssues(volumeId).then(i => {
            this.Issues = i;
            this.setIssue(i[i.length - 1]);
        });
    }
}