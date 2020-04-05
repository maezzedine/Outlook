import { Vue, Component, Watch } from 'vue-property-decorator';
import mainMenu from '@/components/main-menu/MainMenu.vue';

@Component({
    name: 'Home',
    components: { mainMenu },
    data() {
        return {
            Volume: undefined,
            Issue: undefined
        }
    }
})
export default class Home extends Vue {
    created() {
        this.UpdateIssue();
        this.UpdateVolume();
    }

    @Watch('$parent.$data.Issue')
    UpdateIssue() {
        this.$data.Issue = this.$parent.$data.Issue;
    }

    @Watch('$parent.$data.Volume')
    UpdateVolume() {
        this.$data.Volume = this.$parent.$data.Volume;
    }

    volumeToString() {
        var volume = this.$data.Volume;
        if (volume != undefined) {
            return 'Volume ' + volume.volumeNumber + ' | ' + volume.fallYear + ' - ' + volume.springYear;
        }
        return 'No Volume Selceted';
    }
} 