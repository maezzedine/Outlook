import { Vue, Component, Prop } from 'vue-property-decorator';
import svgOutlook from '@/components/svgs/svg-outlook.vue';
import { ApiObject } from '../../models/apiObject';

@Component({
    components: { svgOutlook },
})
export default class Navbar extends Vue {
    @Prop() language!: ApiObject;
}