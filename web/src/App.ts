import { Component, Vue, Watch } from 'vue-property-decorator';
import { initializeTheming, getTheme, getCurrentTheme, setTheme } from 'css-theming';
import { api } from './services/api';
import Home from './views/Home/Home.vue';
import outlookNavbar from '@/components/navbar/Navbar.vue';
import { Language } from '@/models/language';

@Component({
    name: 'App',
    components: {
        Home, outlookNavbar
    },
    data() {
        return {
            Language: undefined
        }
    }
})
export default class App extends Vue {
    private lang: string = "en";

    async created() {
        await this.getLanguage();
        this.setPageSpecifications();

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

    async toggleLang() {
        this.lang = (this.lang == 'en') ? 'ar' : 'en';
        await this.getLanguage();
    }

    async getLanguage() {
        this.$data.Language = await api.getLanguageFile(this.lang);
    }

    @Watch('$data.Language')
    setPageSpecifications() {
        document.body.dir = this.$data.Language.dir;
        document.body.style.fontFamily = this.$data.Language.font;
        document.body.lang = this.$data.Language.lang;
    }

}

/*

  {
  "dir": "rtl",
  "font-family": "Amiri",
  "serif;": null,
  "search": "????",
  "volumes": "??????",
  "issues": "?????",
  "about": "???? ??? ??????",
  "language": "????????",
  "volume": "????",
  "issue": "???"
}
*/
