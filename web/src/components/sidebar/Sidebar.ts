import { Vue, Component, Watch } from 'vue-property-decorator';
import { ApiObject } from '@/models/apiObject';
import { api } from '@/services/api';
import svgHome from '@/components/svgs/svg-home.vue';
import svgQuill from '@/components/svgs/svg-quill.vue';
import svgMeeting from '@/components/svgs/svg-meeting.vue';
import svgContribute from '@/components/svgs/svg-contribute.vue';
import svgInfo from '@/components/svgs/svg-info.vue';
import svgMoon from '@/components/svgs/svg-moon.vue';
import svgSun from '@/components/svgs/svg-sun.vue';
import svgCategory from '@/components/svgs/svg-category.vue';
import svgClose from '@/components/svgs/svg-close.vue';

@Component({
    components: { svgHome, svgQuill, svgMeeting, svgContribute, svgInfo, svgMoon, svgSun, svgCategory, svgClose },
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
        return cat.language == this.$store.getters.Language['language']
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
        return this.Icons[cat.tag]
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