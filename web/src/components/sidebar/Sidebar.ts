import { Vue, Component, Prop, Watch } from 'vue-property-decorator';
import { Issue } from '@/models/issue';
import { Category } from '@/models/category';
import { api } from '@/services/api';
import { getCurrentTheme, setTheme, Theme } from 'css-theming';
import { Language } from '../../models/language';
import { Icons } from '../../models/icons';

@Component
export default class SideBar extends Vue {
    private Categories = new Array<Category>();
    private Icons: Icons | null = null;
    private theme: string | null = null;

    @Prop() language!: Language;

    created() {
        this.updateTheme();
        this.getIcons();
        this.getCategories();
        this.addShadow();
        this.collapseWhenSmall();
    }

    @Watch('issue')
    getCategories() {
        api.getCategories().then(r => {
            this.Categories = r;
        });
    }

    @Watch('$parent.$data.Theme')
    updateTheme() {
        this.theme = this.$parent.$data.Theme;
    }

    getCategoryLanguage(lang: Number) {
        return (lang == 0) ? 'ar' : 'en';
    }

    showCategory(cat: Category) {
        return this.getCategoryLanguage(cat.language) == this.language.lang
    }

    async getIcons() {
        this.Icons = await api.getIcons();
    }

    getCategoryIcon(cat: Category) {
        if (this.Icons == null) {
            return "";
        }
        return this.Icons[cat.tagName]
    }

    @Watch('$props.language')
    addShadow() {
        var sidebar = document.getElementById('sidebar');
        console.log(sidebar);
        if (sidebar != null) {
            if (document.body.dir == 'rtl') {
                sidebar.classList.remove('box-shadow-right');
                sidebar.classList.add('box-shadow-left');
            }
            else {
                sidebar.classList.remove('box-shadow-left');
                sidebar.classList.add('box-shadow-right');
            }
        }
    }

    collapseWhenSmall() {
        if (window.matchMedia('(max-width: 900px)').matches) {
            var sidebar = document.getElementById('sidebar');
            if (sidebar != null) {
                sidebar.classList.add('collapsed');
            }
        }
    }
}