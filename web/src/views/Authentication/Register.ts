import { Component, Vue } from "vue-property-decorator";
import { authService } from '@/services/auth-service';
import RegisterModel from '../../models/registerModel';

@Component
export default class Login extends Vue {
    private Model = new RegisterModel();
    private token = "";
    private error = "";

    created() {
    }

    register() {
        authService.Register(this.Model).then(d => {
            if (d != undefined) {
                console.log(d.access_token);
                this.token = d.access_token;
            }
        })
    }
}