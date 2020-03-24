import { Vue, Component } from 'vue-property-decorator';
import navbar from '@/components/navbar/navbar.vue';

@Component({
    components: { navbar }
})
export default class Home extends Vue {
    
} 