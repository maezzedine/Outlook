import { Component, Vue, Watch } from 'vue-property-decorator';
import { initializeTheming, getTheme, getCurrentTheme, setTheme, Theme } from 'css-theming';
import { api } from './services/api';
import { cacher } from './services/cacher';
import Home from './views/Home/Home.vue';
import outlookNavbar from '@/components/navbar/Navbar.vue';
import outlookSidebar from '@/components/sidebar/Sidebar.vue';
import { ApiObject } from './models/apiObject';

@Component({
    name: 'App',
    components: {
        Home, outlookNavbar, outlookSidebar
    },
    data() {
        return {
            Language: {},
            Colors: undefined,
            Issue: undefined,
            Volume: undefined,
            Theme: 'default'
        }
    }
})
export default class App extends Vue {
    private lang: string | null = null;
    private expanded = false || screen.width > 700;

    private Articles = new ApiObject();

    created() {
        this.initializeLanguageFromCache();
        this.getLanguage();
        this.intializeThemeFromCache();
        this.initializeVolumes();
        this.addBoxShadow();
        this.getColors();
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
        this.addBoxShadow();
    }

    getLanguage() {
        api.getLanguageFile(this.lang).then(d => {
            this.$data.Language = d;
        });
    }

    getColors() {
        api.getColors().then(d => {
            this.$data.Colors = d;
        });
    }

    initializeLanguageFromCache() {
        var localLanguage = localStorage.getItem('language');
        this.lang = (localLanguage != null) ? localLanguage : "en";
    }

    intializeThemeFromCache() {
        var localTheme = localStorage.getItem('theme');
        this.$data.Theme = (localTheme != null) ? localTheme : 'default';
        initializeTheming(getTheme(this.$data.Theme));
    }

    initializeVolumes() {
        api.getVolumeNumbers().then(d => {
            this.initializeVolume(d);
        });
    }

    initializeIssues() {
        api.getIssues(parseInt(this.$data.Volume['id'])).then(i => {
            this.initializeIssue(i);
        });
    }

    initializeIssue(issues: Array<ApiObject>) {
        this.$data.Issue = cacher.initializeIssue(issues);
        this.getArticles();
    }

    initializeVolume(volumes: Array<ApiObject>) {
        this.$data.Volume = cacher.initializeVolume(volumes);
        this.initializeIssues();
    }

    setIssue(issue: ApiObject) {
        this.$data.Issue = issue;
        sessionStorage.setItem('Outlook-Issue', this.$data.Issue['id']);
    }

    setVolume(volume: ApiObject) {
        this.$data.Volume = volume;
        sessionStorage.setItem('Outlook-Volume', this.$data.Volume['id']);
    }

    addBoxShadow() {
        var sidebar = document.getElementById('sidebar');
        var stats = document.getElementById('stats');

        if (sidebar != null && stats != null) {
            if (document.body.dir == 'rtl') {
                sidebar.classList.add('box-shadow-left');
                sidebar.classList.remove('box-shadow-right');

                stats.classList.add('box-shadow-right');
                stats.classList.remove('box-shadow-left');
            }
            else {
                sidebar.classList.add('box-shadow-right');
                sidebar.classList.remove('box-shadow-left');

                stats.classList.add('box-shadow-left');
                stats.classList.remove('box-shadow-right');
            }
        }
    }

    @Watch('$data.Issue')
    getArticles() {
        api.getArticles(this.$data.Issue['id']).then(a => {
            this.Articles = a;
        })
    }

    @Watch('$data.Language')
    setPageSpecifications() {
        document.body.style.fontFamily = this.$data.Language.font;
        document.body.lang = this.$data.Language.lang;
        document.body.dir = this.$data.Language.dir;

        this.toggleDivClasses('navbar', 'ar-expand', 'en-expand', true);
        this.toggleDivClasses('route', 'ar-expand', 'en-expand', true);
        this.toggleDivClasses('sidebar', 'ar-hide', 'en-hide', true);
    }

    toggleExpansion() {
        this.toggleDivClasses('svg-outlook', 'svg-outlook-rotate-left', 'svg-outlook-rotate-right', false);

        if (screen.width < 700) {
            this.expanded = !this.expanded;
        }
    }

    // if toggeling is based on the language then class_a is the one to set for dir = rtl
    toggleDivClasses(id: string, class_a: string, class_b: string, languageDependent: boolean) {
        var div = document.getElementById(id);
        if (div != null) {
            var classes = div.classList;
            if (languageDependent) {
                var dir = this.$data.Language.dir;
                this.removeFirstAddSecond(classes, class_b, class_a, dir == 'rtl');
            } else {
                this.removeFirstAddSecond(classes, class_a, class_b, classes.contains(class_a));
            }
        }
    }

    removeFirstAddSecond(classes: DOMTokenList, class_a: string, class_b: string, condition: boolean) {
        if (condition) {
            classes.remove(class_a);
            classes.add(class_b);
        }
        else {
            classes.remove(class_b);
            classes.add(class_a);
        }
    }
}
