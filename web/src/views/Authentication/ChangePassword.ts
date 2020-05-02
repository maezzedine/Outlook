import { Component, Vue } from "vue-property-decorator";
import ChangePasswordModel from '../../models/changePasswordModel';
import { authService } from '../../services/auth-service';

@Component
export default class ChangePassword extends Vue {
    private Model = new ChangePasswordModel();
    private errors = new Array<string>();
    private succeeded = false;

    changePassword() {
        authService.changePassword(this.Model)
            .then(d => {
                this.succeeded = d.succeeded;
                this.errors = new Array<string>();

                if (!d.succeeded) {
                    for (var error of d.errors) {
                        this.errors.push(error.description)
                    }
                }
            })
    }
}