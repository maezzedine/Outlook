import { Component, Vue, Watch } from "vue-property-decorator";
import { ApiObject } from '../../models/apiObject';
import { api } from '../../services/api';

@Component
export default class OutlookWriters extends Vue {
    private Writers: Array<ApiObject> | null = null;

    created() {
        this.getWriters();
    }

    getWriters() {
        var writersFromSession = sessionStorage.getItem('outlook-writers');
        if (writersFromSession != null) {
            var writers = JSON.parse(writersFromSession);
            this.Writers = writers;
        }
        else {
            api.Get('members').then(d => {
                sessionStorage.setItem('outlook-writers', JSON.stringify(d));
                this.Writers = d;
            });
        }
        
    }
}