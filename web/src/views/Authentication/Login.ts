import { Component, Vue } from "vue-property-decorator";
import { authService } from '@/services/auth-service';
import LoginModel from '../../models/loginModel';

@Component
export default class Login extends Vue {
    private Model = new LoginModel();
    private token = "";
    private error = "";

    created() {
    }

    login() {
        authService.Login(this.Model)
            .then(data => {
                console.log(data);
                this.error = data.data.error_description;
                this.token = data.data.access_token
            });
            
    }
}