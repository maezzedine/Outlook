import { Component, Vue } from 'vue-property-decorator';
import Home from './views/Home/Home.vue';
import { initializeTheming, getTheme, getCurrentTheme, setTheme } from 'css-theming';

@Component({
    name: 'App',
    components: {
        Home
    }
})
export default class App extends Vue {
    created() {
        initializeTheming(getTheme("default"));

        document.addEventListener('keypress', e => {
            if (e.defaultPrevented) return;

            if (e.key === 't') {
                const previousTheme = getCurrentTheme();
                const newTheme = previousTheme.name == 'default' ? 'default-dark' : 'default';
                setTheme(getTheme(newTheme));
            }
        });
    }
}