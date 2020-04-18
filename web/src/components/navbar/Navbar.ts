import { Vue, Component, Prop, Watch } from 'vue-property-decorator';
import svgOutlook from '@/components/svgs/svg-outlook.vue';
import { ApiObject } from '../../models/apiObject';

@Component({
    components: { svgOutlook },
})
export default class Navbar extends Vue {
    private expanded = false;

    @Watch("$parent.$data.expanded")
    UpdateExpansion() {
        this.expanded = this.$parent.$data.expanded;
    }
}