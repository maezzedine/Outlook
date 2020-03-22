import { Component, Vue } from 'vue-property-decorator';
import Home from './views/Home/Home.vue';
import { initializeTheming } from 'css-theming';

@Component({
    components: {
        Home
    }
    
})
export default class extends Vue {
    created() {
        initializeTheming();
    }
}