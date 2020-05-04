import { Component, Vue } from "vue-property-decorator";
import { authService } from '@/services/auth-service';
import LoginModel from '../../models/loginModel';

@Component
export default class Login extends Vue {
    private Model = new LoginModel();
    private errors = new Array<string>();

    login() {
        this.errors = new Array<string>();
        var validInput = this.inputIsValid();

        if (validInput) {
            authService.Login(this.Model)
                .catch(e => {
                    authService.Logout();

                    e.then(f => {
                        this.errors.push(f.error_description.replace(/_/g, ' '));
                     })
                });
        }
    }

    inputIsValid() {
        var properties = JSON.parse(this.Model.properties);
        var valid = true;
        for (var attribute of properties) {
            var attributeIsValid = this.notNullOrEmpty(this.Model[attribute]);
            if (!attributeIsValid) {
                this.errors.push(this.$store.getters.Language[`${attribute}-required`]);
            }
            valid = valid && attributeIsValid;
        }
        return valid;
    }

    notNullOrEmpty(item: string) {
        return item != undefined && item != '';
    }
}
