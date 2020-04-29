import { Component, Vue, Watch } from "vue-property-decorator";
import { authService } from '@/services/auth-service';
import LoginModel from '../../models/loginModel';
import outlookUser from '../../models/outlookUser';

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
                    var user = new outlookUser();
                    user.username = this.Model.username;
                    user.token = response.access_token;
                    this.$store.dispatch('setUser', user);
                    localStorage.setItem('outlook-user', JSON.stringify(user));
                    this.signInSuccessfuly = true;
                })
                .catch(e => {
                    this.$store.dispatch('removeUser');
                    this.signInSuccessfuly = false;
                    localStorage.setItem('outlook-user', '');

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
