import { Vue, Component, Prop, Watch } from 'vue-property-decorator';
import svgOutlook from '@/components/svgs/svg-outlook.vue';
import { api } from '@/services/api';
import { Volume } from '../../models/volume';
import { Issue } from '../../models/issue';
import App from '@/App';
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
    }

    getIssues(volumeId: Number) {
        api.getIssues(volumeId).then(i => {
            this.Issues = i;
            this.Issue = i[i.length - 1];
        })
    }

    //@Watch('direction')
    //setPageDirection() {
    //    document.body.dir = this.direction;
    //}
}