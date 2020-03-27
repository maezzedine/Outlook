import { Vue, Component, Prop, Watch } from 'vue-property-decorator';
import { Issue } from '@/models/issue';
import { Category } from '@/models/category';
import { api } from '@/services/api';
import { getCurrentTheme, setTheme, Theme } from 'css-theming';
import { Language } from '../../models/language';

@Component
export default class SideBar extends Vue {
    private Categories = new Array<Category>();
    private theme: string | null = null;

    @Prop() language!: Language;

    created() {
        this.updateTheme();
        //this.updateLanguage();
        this.getCategories();
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

    //@Watch('$parent.$data.Language')
    //updateLanguage() {
    //    this.lanugage = this.$parent.$data.Language;
    //}

    getCategoryLanguage(lang: Number) {
        return (lang == 0) ? 'ar' : 'en';
    }

    showCategory(cat: Category) {
        return this.getCategoryLanguage(cat.language) == this.language.lang
    }
}