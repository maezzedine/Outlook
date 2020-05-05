import { Vue, Component, Prop, Watch } from 'vue-property-decorator';
import svgOutlook from '@/components/svgs/svg-outlook.vue';
import { authService } from '../../services/auth-service';
import svgSignout from '@/components/svgs/svg-signout.vue';
import svgSignin from '@/components/svgs/svg-signin.vue';
import svgSignup from '@/components/svgs/svg-signup.vue';
import svgArchives from '@/components/svgs/svg-archive.vue';
import svgLanguage from '@/components/svgs/svg-language.vue';
import svgHome from '@/components/svgs/svg-home.vue';

@Component({
    components: { svgOutlook, svgSignout, svgSignin, svgSignup, svgArchives, svgLanguage, svgHome },
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