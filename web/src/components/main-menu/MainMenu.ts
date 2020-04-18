import { Component, Vue, Watch } from "vue-property-decorator";
import { ApiObject } from '../../models/apiObject';
import articleThumbnail from '@/components/article-thumbnail/Article-Thumbnail.vue';


@Component({
    components: { articleThumbnail }
})
export default class MainMenu extends Vue {
    private Articles = new Array<ApiObject>();
    private Volume = new ApiObject();
    private Issue = new ApiObject();

    created() {
        this.updateArticles();
        this.UpdateVolume();
        this.UpdateIssue();
    }

    VolumeToString() {
        var volume = this.$data.Volume;
        if (volume != undefined) {
            return this.$store.getters.Language.volume + ' ' + volume.volumeNumber + ' | ' + volume.fallYear + ' - ' + volume.springYear;
        }
        return 'No Selected Volume';
    }

    IssueToString() {
        var issue = this.$data.Issue;
        if (issue != undefined) {
            return this.$store.getters.Language.issue + ' ' + issue.issueNumber;
        }
        return 'No Selected Issue';
    }

    @Watch('$parent.$parent.$data.Articles')
    updateArticles() {
        this.Articles = this.$parent.$parent.$data.Articles;
    }

    @Watch("$parent.$data.Volume")
    UpdateVolume() {
        this.Volume = this.$parent.$data.Volume;
    }

    @Watch("$parent.$data.Issue")
    UpdateIssue() {
        this.Issue = this.$parent.$data.Issue;
    }
}