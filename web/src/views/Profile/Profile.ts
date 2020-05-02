import { Component, Vue } from "vue-property-decorator";
import { authService } from '../../services/auth-service';
import { ApiObject } from '../../models/apiObject';

@Component
export default class Profile extends Vue {
    private User!: ApiObject;
    private loading = true;

    created() {
        this.getUser();
    }

    getUser() {
        authService.getUser().then(d => {
            this.User = d;
            this.loading = false;
        })
    }
}