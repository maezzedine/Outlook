import { Component, Vue } from "vue-property-decorator";
import { api } from '../../services/api';

@Component
export default class EmailConfirmation extends Vue {
    resendVerification() {
        var params = new Array<string>();
        params.push(this.$store.getters.Username);
        api.AuthorizedAction('POST', 'Identity/ResendVerification', params);
    }
}