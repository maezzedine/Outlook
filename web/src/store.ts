import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

export default new Vuex.Store({
    state: {
        english: Object,
        arabic: Object,
        lang: ''
    },
    mutations: {
        setLang(state, lang) {
            state.lang = lang;
        },
        setEnglish(state, en) {
            state.english = en;
        },
        setArabic(state, ar) {
            state.arabic = ar;
        }
    },
    getters: {
        Language: state => {
            return (state.lang == 'en') ? state.english : state.arabic;
        }
    }
})