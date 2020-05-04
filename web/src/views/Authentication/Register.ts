import { Component, Vue } from "vue-property-decorator";
import { authService } from '@/services/auth-service';
import RegisterModel from '../../models/registerModel';
import { createWriteStream } from 'fs';

@Component
export default class Login extends Vue {
    private Model = new RegisterModel();
    private errors = new Array<string>();

    register() {
        this.errors = new Array<string>();
        var validInput = this.inputIsValid();

        if (validInput) {
            authService.Register(this.Model)
                .then(d => {
                    if (d.succeeded) {
                        var lang = this.$route.params['lang'];
                        this.$router.push(`/${lang}/email-confirmation`);
                    }
                })
                .catch(e => {
                    authService.Logout();

                    var errorList = e.response.data.errors;
                    for (var errors of Object.keys(errorList)) {
                        for (var error of errorList[errors]) {
                            this.errors.push(error.replace(/_/g, ' '));
                        }
                    }
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