import { Component, Vue, Watch } from "vue-property-decorator";
import { authService } from '@/services/auth-service';
import RegisterModel from '../../models/registerModel';
import outlookUser from '../../models/outlookUser';

@Component
export default class Login extends Vue {
    private Model = new RegisterModel();
    private errors = new Array<string>();
    private signInSuccessfuly = false;

    register() {
        this.errors = new Array<string>();
        var validInput = this.inputIsValid();

        if (validInput) {
            authService.Register(this.Model)
                .then(d => {
                    if (d != undefined) {
                        this.signInSuccessfuly = true;
                    }
                })
                .catch(e => {
                    authService.Logout();
                    this.signInSuccessfuly = false;

                    var errorList = e.response.data.errors;
                    console.log(errorList);
                    console.log(Object.keys(errorList));

                    for (var errors of Object.keys(errorList)) {
                        for (var error of errorList[errors]) {
                            this.errors.push(error.replace(/_/g, ' '));
                            console.log(errorList[error]);
                        }
                    }
                });
        }
        else {
            this.checkRequiredItem(this.Model.username, 'username-required');
            this.checkRequiredItem(this.Model.password, 'password-required');
            this.checkRequiredItem(this.Model.firstName, 'firstname-required');
            this.checkRequiredItem(this.Model.lastName, 'lastname-required');
        }
    }

    inputIsValid() {
        return (this.Model.username != undefined) && (this.Model.firstName != undefined) && (this.Model.lastName != undefined) && (this.Model.password != '')
            && (this.Model.password != undefined) && (this.Model.username != '') && (this.Model.firstName != '') && (this.Model.lastName != '')
    }

    notNullOrEmpty(item: string) {
        return item == undefined || item == '';
    }

    checkRequiredItem(item: string, rule: string) {
        if (this.notNullOrEmpty(item)) {
            this.errors.push(this.$store.getters.Language[rule]);
        }
    }
}