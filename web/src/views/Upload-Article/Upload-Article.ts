import { Component, Vue } from "vue-property-decorator";
import { api } from '../../services/api';
import svgCheck from '@/components/svgs/svg-check.vue';

@Component({
    components: { svgCheck }
})
export default class UploadArticle extends Vue {
    private Form = new FormData();
    private ProcessCompleted = false;

    sendFile() {
        this.ProcessCompleted = false;
        api.postFile('articles/upload', this.Form).then(status => {
            if (status > 199 && status < 300) {
                this.ProcessCompleted = true;
            }
        });
    }

    updateFile(file: any) {
        this.Form.append('file', file, file.name);
    }
}