import { Component, Vue } from "vue-property-decorator";
import { authService } from '@/services/auth-service';
import RegisterModel from '../../models/registerModel';
import outlookUser from '../../models/outlookUser';

@Component
export default class Login extends Vue {
    private Model = new RegisterModel();
    private errors = new Array<string>();

    created() {
    }

    register() {
        this.errors = new Array<string>();
        var validInput = this.inputIsValid();

        if (validInput) {
            authService.Register(this.Model)
                .then(d => {
                    if (d != undefined) {
                        var user = new outlookUser();
                        user.username = this.Model.username;
                        user.token = d.access_token;

                        this.$store.dispatch('setUser', user);
                    }
                })
                .catch(e => {
                    this.$store.dispatch('removeUser');
                    for (var error of e.response.data.errors.Password) {
                        this.errors.push(error.replace(/_/g, ' '));
                    }
                });
        }
        else {
            if (this.Model.username == undefined || this.Model.username == '') {
                this.errors.push(this.$store.getters.Language['username-required']);
            }
            if (this.Model.password == undefined || this.Model.password == '') {
                this.errors.push(this.$store.getters.Language['password-required']);
            }
            if (this.Model.firstName == undefined || this.Model.firstName == '') {
                this.errors.push(this.$store.getters.Language['firstname-required']);
            }
            if (this.Model.lastName == undefined || this.Model.lastName == '') {
                this.errors.push(this.$store.getters.Language['lastname-required']);
            }
        }
    }

    inputIsValid() {
        return (this.Model.username != undefined) && (this.Model.firstName != undefined) && (this.Model.lastName != undefined) && (this.Model.password != '')
            && (this.Model.password != undefined) && (this.Model.username != '') && (this.Model.firstName != '') && (this.Model.lastName != '')
    }
}