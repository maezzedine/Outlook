import { Vue, Component } from 'vue-property-decorator';
import outlookNavbar from '@/components/navbar/Navbar.vue';

@Component({
    name: "Home",
    components: { outlookNavbar }
})
export default class Home extends Vue {
} 