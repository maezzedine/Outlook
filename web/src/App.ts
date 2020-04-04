import { Component, Vue, Watch } from 'vue-property-decorator';
import { initializeTheming, getTheme, getCurrentTheme, setTheme, Theme } from 'css-theming';
import { api } from './services/api';
import Home from './views/Home/Home.vue';
import outlookNavbar from '@/components/navbar/Navbar.vue';
import outlookSidebar from '@/components/sidebar/Sidebar.vue';
import { Issue } from './models/issue';

@Component({
    name: 'App',
    components: {
        Home, outlookNavbar, outlookSidebar
    },
    data() {
        return {
            Language: {},
            Issue: null,
            Theme: 'default'
        }
    }
})
export default class App extends Vue {
    private lang: string | null = null;
    private expanded = false || screen.width > 700; // || screen.width > 700

    async created() {
        this.initializeLanguageFromCache();
        await this.getLanguage();
        this.setPageSpecifications();
        this.intializeThemeFromCache();
    }

    toggleTheme() {
        const previousTheme = getCurrentTheme();
        this.$data.Theme = previousTheme.name == 'default' ? 'default-dark' : 'default';
        setTheme(getTheme(this.$data.Theme));
        localStorage.setItem('theme', this.$data.Theme);
    }

    async toggleLang() {
        this.lang = (this.lang == 'en') ? 'ar' : 'en';
        localStorage.setItem('language', this.lang);
        await this.getLanguage();
    }

    async getLanguage() {
        this.$data.Language = await api.getLanguageFile(this.lang);
    }

    initializeLanguageFromCache() {
        if (localStorage.getItem('language') != null) {
            this.lang = localStorage.getItem('language');
        }
        else {
            this.lang = "en";
        }
    }

    intializeThemeFromCache() {
        if (localStorage.getItem('theme') != null) {
            this.$data.Theme = localStorage.getItem('theme');
            initializeTheming(getTheme(this.$data.Theme));
        }
        else {
            initializeTheming(getTheme("default"));
            this.$data.Theme = getCurrentTheme().name;
        }
    }

    setIssue(issue: Issue) {
        this.$data.Issue = issue;
    }

    @Watch('$data.Language')
    setPageSpecifications() {
        document.body.dir = this.$data.Language.dir;
        document.body.style.fontFamily = this.$data.Language.font;
        document.body.lang = this.$data.Language.lang;
    }

    toggleExpansion() {
        var logo = document.getElementById('svg-outlook')!;

        if (logo.classList.contains('svg-outlook-rotate-left')) {
            logo.classList.remove('svg-outlook-rotate-left');
            logo.classList.add('svg-outlook-rotate-right');
        } else {
            logo.classList.remove('svg-outlook-rotate-right');
            logo.classList.add('svg-outlook-rotate-left');
        }

        if (screen.width < 700) {
            this.expanded = !this.expanded;
        }
    }
}
