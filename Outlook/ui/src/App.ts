import { Component, Vue } from 'vue-property-decorator';
import Home from './views/Home/Home.vue';
import { initializeTheming, getTheme, getThemes } from 'css-theming';

@Component({
    components: {
        Home
    },
    data() {
        return {
            themes: getThemes()
        }
    }
    
})
export default class extends Vue {
    created() {
        initializeTheming(getTheme("default"));
        //initializeTheming(getTheme("default-dark"));
        //setTheme(getTheme("default-dark"));


    }
}