import { Component, Vue, Watch } from "vue-property-decorator";
import { authService } from '@/services/auth-service';
import LoginModel from '../../models/loginModel';
import { isObject } from 'util';

@Component
export default class Login extends Vue {
    private Model = new LoginModel();
    private errors = new Array<string>();
    private signInSuccessfuly = false;

    login() {
        this.errors = new Array<string>();
        var validInput = this.inputIsValid();

        if (validInput) {
            authService.Login(this.Model)
                .then(response => {
                    this.signInSuccessfuly = true;
                })
                .catch(e => {
                    this.signInSuccessfuly = false;
                    authService.Logout();

                    e.then(f => {
                        this.errors.push(f.error_description.replace(/_/g, ' '));
                     })
                });
        }
        else {
            if (this.Model.username == undefined || this.Model.username == '') {
                this.errors.push(this.$store.getters.Language['username-required']);
            }
            if (this.Model.password == undefined || this.Model.password == '') {
                this.errors.push(this.$store.getters.Language['password-required']);
            }
        }
    }

    inputIsValid() {
        return (this.Model.username != undefined) && (this.Model.password != undefined) && (this.Model.username != '') && (this.Model.password != '')
    }
}
