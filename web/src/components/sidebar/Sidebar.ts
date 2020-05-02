import { Vue, Component, Prop, Watch } from 'vue-property-decorator';
import { ApiObject } from '@/models/apiObject';
import { api } from '@/services/api';
import { getCurrentTheme, setTheme, Theme } from 'css-theming';

@Component({
    data() {
        return {
            Categories: undefined
        }
    }
})
export default class SideBar extends Vue {
    private Icons: ApiObject | null = null;
    private theme: string | null = null;
    private expanded = false;

    created() {
        this.updateTheme();
        this.getIcons();
        this.getCategories();
        this.collapseWhenSmall();
    }

    @Watch('$parent.$data.Categories')
    getCategories() {
        if (this.$parent.$data.Categories != undefined) {
            this.$data.Categories = this.$parent.$data.Categories;
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
        return this.getCategoryLanguage(parseInt(cat.language)) == this.$store.getters.Language.lang
    }

    getIcons() {
        api.getLocalJsonFile('font-awesome').then(d => {
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

    @Watch("$parent.$data.expanded")
    UpdateExpansion() {
        this.expanded = this.$parent.$data.expanded;
    }
}