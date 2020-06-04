import { Component, Vue } from "vue-property-decorator";
import { authService } from '../../services/auth-service';

@Component
export default class EmailConfirmation extends Vue {
    resendVerification() {
        console.log('fired');
        //var params = new Array<string>();
        //params.push(this.$store.getters.Username);
        //api.AuthorizedAction('POST', 'Identity/ResendVerification', params);
        authService.ResendEmailVerification(this.$store.getters.Username);
    }
}