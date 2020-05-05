import { Component, Vue, Prop, Watch } from "vue-property-decorator";
import { ApiObject } from '@/models/apiObject';
import TopModel from '../../models/topModel';
import svgStats from '@/components/svgs/svg-stats.vue';

@Component({
    components: { svgStats },
    props: {
        model: {
            type: TopModel,
            private: true,
            default() { return new TopModel('', '', '', new Array <ApiObject>(), '', '', '' ); }
        }
    }
})
export default class TopArticles extends Vue {
    
}