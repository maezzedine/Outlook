import Vue from 'vue';
import Vuex, { ActionContext } from 'vuex';

Vue.use(Vuex);

interface languageState {
    lang: string;
    english: object;
    arabic: object;
}

const state = {
    english: Object,
    arabic: Object,
    lang: ''
}

const mutations = {
    setLang(state: languageState, lang: string) {
        state.lang = lang;
    },
    setEnglish(state: languageState, en: languageState) {
        state.english = en;
    },
    setArabic(state: languageState, ar: languageState) {
        state.arabic = ar;
    }
}

const actions = {
    setLang(context: ActionContext<languageState, languageState>, lang: string) {
        context.commit('setLang', lang);
    },
    setEnglish(context: ActionContext<languageState, languageState>, en: languageState) {
        context.commit('setEnglish', en);
    },
    setArabic(context: ActionContext<languageState, languageState>, ar: languageState) {
        context.commit('setArabic', ar);
    }
}

const getters = {
    Language: (state: languageState) => {
        return (state.lang == 'en') ? state.english : state.arabic;
    }
}

export default {
    namespaced: true,
    state,
    mutations,
    actions,
    getters
}
   