import { Component, Vue } from "vue-property-decorator";
import { authService } from '@/services/auth-service';
import LoginModel from '../../models/loginModel';
import outlookUser from '../../models/outlookUser';

@Component
export default class Login extends Vue {
    private Model = new LoginModel();
    private error = "";

    created() {
    }

    login() {
        authService.Login(this.Model)
            .then(response => {
                this.error = '';

                var user = new outlookUser();
                user.username = this.Model.username;
                user.token = response.access_token;

                this.$store.dispatch('setUser', user);
            })
            .catch(e => {
                this.$store.dispatch('removeUser');

                e.then(f => {
                    if (f.error = 'invalid_grant') {
                        this.error = f.error_description.replace(/_/g, ' ');
                    }
                })
            });
    }
}