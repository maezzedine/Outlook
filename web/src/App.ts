import { Component, Vue, Watch } from 'vue-property-decorator';
import { initializeTheming, getTheme, getCurrentTheme, setTheme } from 'css-theming';
import { api } from './services/api';
import { cacher } from './services/cacher';
import Home from './views/Home/Home.vue';
import outlookNavbar from '@/components/navbar/Navbar.vue';
import outlookSidebar from '@/components/sidebar/Sidebar.vue';
import svgSpinner from '@/components/svgs/svg-spinner.vue';
import { ApiObject } from './models/apiObject';
import outlookUser from './models/outlookUser';
import { ArticleScoreChange } from './models/articleScoreChange';
import { ArticleCommentChange } from './models/articleCommentChange';
import { ArticleFavoriteChange } from './models/articleFavoriteChange';

@Component({
    name: 'App',
    components: {
        Home, outlookNavbar, outlookSidebar, svgSpinner
    },
    data() {
        return {
            Issue: undefined,
            Volume: undefined,
            Categories: undefined,
            Articles: undefined,
            Theme: 'default',
        }
    }
})
export default class App extends Vue {
    private lang: string | null = null;
    private expanded = false || screen.width > 700;
    private loading = false;

    created() {
        this.initializeStateLanguages();
        this.intializeThemeFromCache();
        this.initializeVolumes();
        this.getColors();
        this.getCategories();
        this.CheckIfAuthenticated();

        this.$articleHub.$on('article-score-changed', this.onArticleScoreChange);
        this.$articleHub.$on('article-comment-changed', this.onArticleCommentChange);
        this.$articleHub.$on('article-favorite-changed', this.onArticleFavoriteChange);
    }

    // Theme
    toggleTheme() {
        const previousTheme = getCurrentTheme();
        this.$data.Theme = previousTheme.name == 'default' ? 'default-dark' : 'default';
        setTheme(getTheme(this.$data.Theme));
        localStorage.setItem('theme', this.$data.Theme);
    }

    intializeThemeFromCache() {
        var localTheme = localStorage.getItem('theme');
        this.$data.Theme = (localTheme != null) ? localTheme : 'default';
        initializeTheming(getTheme(this.$data.Theme));
    }

    // Language
    initializeStateLanguages() {
        var enFromCache = sessionStorage.getItem('outlook-en');
        var arFromCache = sessionStorage.getItem('outlook-ar');
        if (enFromCache != undefined && arFromCache != undefined) {
            this.$store.dispatch('setEnglish', JSON.parse(enFromCache));
            this.$store.dispatch('setArabic', JSON.parse(arFromCache));
            this.initializeLanguage();
        }
        else {
            this.loading = true;
            api.getLocalJsonFile('en').then(e => {
                this.$store.dispatch('setEnglish', e);
                sessionStorage.setItem('outlook-en', JSON.stringify(e));

                api.getLocalJsonFile('ar').then(a => {
                    this.$store.dispatch('setArabic', a);
                    sessionStorage.setItem('outlook-ar', JSON.stringify(a));
                    this.initializeLanguage();
                    this.loading = false;
                });
            });
        }
    }

    initializeLanguage() {
        var fromParams = this.$route.params.lang;
        if (fromParams != undefined && (fromParams == 'en' || fromParams == 'ar')) {
            this.lang = fromParams;
        }
        else {
            var localLanguage = localStorage.getItem('language');
            this.lang = (localLanguage != null) ? localLanguage : "en";
            this.$router.push('/' + this.lang);
        }
        this.commitStateLanguage();
    }

    toggleLang() {
        this.lang = (this.lang == 'en') ? 'ar' : 'en';
        localStorage.setItem('language', this.lang);
        this.commitStateLanguage();
        this.$router.push('/' + this.lang);
    }

    commitStateLanguage() {
        this.$store.dispatch('setLang', this.lang);
        this.setPageSpecifications();
    }

    setPageSpecifications() {
        document.body.style.fontFamily = this.$store.getters.Language.font;
        document.body.lang = this.$store.getters.Language.lang;
        document.body.dir = this.$store.getters.Language.dir;
    }

    // Colors
    getColors() {
        api.getLocalJsonFile('category-color').then(d => {
            this.$store.dispatch('setColors', d);
        }); 
    }

    // Volumes
    initializeVolumes() {
        api.Get('volumes').then(d => {
            this.initializeVolume(d);
        });
    }

    initializeVolume(volumes: Array<ApiObject>) {
        this.$data.Volume = cacher.initializeVolume(volumes);
        this.initializeIssues();
    }

    setVolume(volume: ApiObject) {
        this.$data.Volume = volume;
        sessionStorage.setItem('Outlook-Volume', this.$data.Volume['id']);
    }

    // Issues
    initializeIssues() {
        var params = new Array<Number>();
        params.push(parseInt(this.$data.Volume['id']))

        api.Get('issues', params).then(i => {
            this.initializeIssue(i);
        });
    }

    initializeIssue(issues: Array<ApiObject>) {
        this.$data.Issue = cacher.initializeIssue(issues);
        this.getArticles();
    }

    setIssue(issue: ApiObject) {
        this.$data.Issue = issue;
        sessionStorage.setItem('Outlook-Issue', this.$data.Issue != undefined? this.$data.Issue['id'] : null);
    }

    @Watch('$data.Issue')
    getArticles() {
        if (this.$data.Issue != null) {
            var params = new Array<Number>();
            params.push(this.$data.Issue['id'])
            api.Get('articles', params).then(a => {
                this.$data.Articles = a;
                this.getCategories();
            })
        }
    }

    onArticleScoreChange(articleScoreChange: ArticleScoreChange) {
        for (var article of this.$data.Articles) {
            if (article.id == articleScoreChange.articleId) {
                this.$store.dispatch('updateArticleRate', articleScoreChange);
                article.rate = articleScoreChange.rate;
                article.numberOfVotes = articleScoreChange.numberOfVotes;
                return;
            }
        }
        // If we reachthis point, then we are viewing an article from outside the chosen issue, thus we have to update the values in the store
        this.$store.dispatch('updateArticleRate', articleScoreChange);
    }

    onArticleCommentChange(articleCommentChange: ArticleCommentChange) {
        for (var article of this.$data.Articles) {
            if (article.id == articleCommentChange.articleId) {
                this.$store.dispatch('updateArticleComments', articleCommentChange);
                article.comments = articleCommentChange.comments;
                return;
            }
        }
        // If we reachthis point, then we are viewing an article from outside the chosen issue, thus we have to update the values in the store
        this.$store.dispatch('updateArticleComments', articleCommentChange);
    }

    onArticleFavoriteChange(articleFavoriteChange: ArticleFavoriteChange) {
        for (var article of this.$data.Articles) {
            if (article.id == articleFavoriteChange.articleId) {
                this.$store.dispatch('updateArticleFavorites', articleFavoriteChange);
                article.numberOfFavorites = articleFavoriteChange.numberOfFavorites;
                return;
            }
        }
        // If we reachthis point, then we are viewing an article from outside the chosen issue, thus we have to update the values in the store
        this.$store.dispatch('updateArticleFavorites', articleFavoriteChange);
    }

    updateArticleInStore(method: string, changes: object) {
        this.$store.dispatch(method, changes);
    }

    // Categories
    getCategories() {
        if (this.$data.Issue != undefined) {
            var params = new Array<Number>();
            params.push(this.$data.Issue.id);

            api.Get('categories', params).then(r => {
                this.$data.Categories = r;
            });
        }
    }

    // Toggle Expansion
    toggleExpansion() {
        this.toggleDivClasses('svg-outlook', 'svg-outlook-rotate-left', 'svg-outlook-rotate-right');

        if (screen.width < 700) {
            this.expanded = !this.expanded;
        }
    }

    // if toggeling is based on the language then class_a is the one to set for dir = rtl
    toggleDivClasses(id: string, class_a: string, class_b: string) {
        var div = document.getElementById(id);
        if (div != null) {
            var classes = div.classList;
            if (classes.contains(class_a)) {
                classes.remove(class_a);
                classes.add(class_b);
            }
            else {
                classes.remove(class_b);
                classes.add(class_a);
            }
        }
    }

    // Authentication
    async CheckIfAuthenticated() {
        var localStorageUser = localStorage.getItem('outlook-user');
        if (localStorageUser != null) {
            var user: outlookUser = JSON.parse(localStorageUser);
            var expiration = new Date(user.expirayDate);
            var now = new Date();
            if (expiration.valueOf() - now.valueOf() > 86400000) {
                this.$store.dispatch('setUser', user);
            }
        }
    }
}

window.onorientationchange = () => {
    location.reload();
}
