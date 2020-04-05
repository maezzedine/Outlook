import { Vue, Component, Prop, Watch } from 'vue-property-decorator';
import svgOutlook from '@/components/svgs/svg-outlook.vue';
import { ApiObject } from '../../models/apiObject';

@Component({
    components: { svgOutlook },
})
export default class Navbar extends Vue {
    private Language = new ApiObject();

    created() {
        this.UpdateLanguage();
    }

    @Watch("$parent.$data.Language")
    UpdateLanguage() {
        this.Language = this.$parent.$data.Language;
    }
}