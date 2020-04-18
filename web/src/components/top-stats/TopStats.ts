import { Component, Vue, Prop, Watch } from "vue-property-decorator";
import { ApiObject } from '@/models/apiObject';
import TopModel from '../../models/topModel';

@Component({
    props: {
        model: {
            type: TopModel,
            private: true,
            default() { return new TopModel('', '', '', new Array <ApiObject>(), '', '' ); }
        }
    }
})
export default class TopArticles extends Vue { }