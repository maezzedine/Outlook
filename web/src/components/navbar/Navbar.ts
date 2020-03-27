import { Vue, Component, Prop } from 'vue-property-decorator';
import svgOutlook from '@/components/svgs/svg-outlook.vue';
import { api } from '@/services/api';
import { Volume } from '../../models/volume';
import { Issue } from '../../models/issue';
import { Language } from '../../models/language';

@Component({
    components: { svgOutlook },
})
export default class Navbar extends Vue {
    private Volumes = new Array<Volume>();
    private Volume!: Volume;

    private Issues = new Array<Issue>();
    private Issue!: Issue;

    @Prop() language!: Language;
    
    created() {
        api.getVolumeNumbers().then(d => {
            this.Volumes = d;
            this.setVolume(d[d.length - 1]);
        });
    }

    setVolume(volume: Volume) {
        this.Volume = volume;
        this.getIssues(volume.id);
    }

    setIssue(issue: Issue) {
        this.Issue = issue;
        this.$emit('set-issue', issue);
    }

    getIssues(volumeId: Number) {
        api.getIssues(volumeId).then(i => {
            this.Issues = i;
            this.setIssue(i[i.length - 1]);
        });
    }
}