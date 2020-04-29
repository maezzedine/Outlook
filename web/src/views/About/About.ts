import { Component, Vue } from "vue-property-decorator";
import svgGithub from '@/components/svgs/svg-github.vue';

@Component({
    components: { svgGithub }
})
export default class About extends Vue { }