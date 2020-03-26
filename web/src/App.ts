import { Component, Vue } from 'vue-property-decorator';
import Home from './views/Home/Home.vue';
import { initializeTheming, getTheme, getCurrentTheme, setTheme } from 'css-theming';
import outlookNavbar from '@/components/navbar/Navbar.vue';

@Component({
    name: 'App',
    components: {
        Home, outlookNavbar
    }
})
export default class App extends Vue {
    created() {
        //initializeTheming(getTheme("default-dark"));
        initializeTheming(getTheme("default"));

        document.addEventListener('keypress', e => {
            if (e.defaultPrevented) return;

            if (e.key === 't') {
                this.toggleTheme();
            }
        });
    }

    toggleTheme() {
        var logo = document.getElementById('svg-outlook')!;
        if (logo.classList.contains('svg-outlook-rotate-right')) {
            logo.classList.remove('svg-outlook-rotate-right');
            logo.classList.add('svg-outlook-rotate-left');
        } else {
            logo.classList.remove('svg-outlook-rotate-left');
            logo.classList.add('svg-outlook-rotate-right');
        }
       

        const previousTheme = getCurrentTheme();
        const newTheme = previousTheme.name == 'default' ? 'default-dark' : 'default';
        setTheme(getTheme(newTheme));

    }
}