import { Vue, Component, Prop, Watch } from 'vue-property-decorator';
import svgOutlook from '@/components/svgs/svg-outlook.vue';
import { authService } from '../../services/auth-service';

@Component({
    components: { svgOutlook },
})
export default class Navbar extends Vue {
    private expanded = false;

    @Watch("$parent.$data.expanded")
    UpdateExpansion() {
        this.expanded = this.$parent.$data.expanded;
    }

    logout() {
        authService.Logout();
    }
}