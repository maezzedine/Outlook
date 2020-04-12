import { Vue, Component, Prop, Watch } from 'vue-property-decorator';
import { ApiObject } from '@/models/apiObject';
import { api } from '@/services/api';
import { getCurrentTheme, setTheme, Theme } from 'css-theming';

@Component
export default class SideBar extends Vue {
    private Categories = new Array<ApiObject>();
    private Icons: ApiObject | null = null;
    private theme: string | null = null;
    private Language = new ApiObject();

    created() {
        this.updateTheme();
        this.getIcons();
        this.getCategories();
        this.collapseWhenSmall();
        this.UpdateLanguage();
    }

    @Watch('$parent.$data.Issue')
    getCategories() {
        if (this.$parent.$data.Issue != undefined) {
            api.getCategories(this.$parent.$data.Issue.id).then(r => {
                this.Categories = r;
            });
        }
    }

    @Watch('$parent.$data.Theme')
    updateTheme() {
        this.theme = this.$parent.$data.Theme;
    }

    getCategoryLanguage(lang: Number) {
        return (lang == 0) ? 'ar' : 'en';
    }

    showCategory(cat: ApiObject) {
        return this.getCategoryLanguage(parseInt(cat.language)) == this.Language.lang
    }

    getIcons() {
        api.getIcons().then(d => {
            this.Icons = d; 
        });
    }

    getCategoryIcon(cat: ApiObject) {
        if (this.Icons == null) {
            return "";
        }
        return this.Icons[cat.tagName]
    }

    collapseWhenSmall() {
        if (window.matchMedia('(max-width: 900px)').matches) {
            var sidebar = document.getElementById('sidebar');
            if (sidebar != null) {
                sidebar.classList.add('collapsed');
            }
        }
    }

    @Watch("$parent.$data.Language")
    UpdateLanguage() {
        this.Language = this.$parent.$data.Language;
    }
}