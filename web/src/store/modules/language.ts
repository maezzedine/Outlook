import { ActionContext } from 'vuex';

const state = {
    english: Object,
    arabic: Object,
    lang: ''
}

const mutations = {
    setLang(state: state, lang: string) {
        state.lang = lang;
    },
    setEnglish(state: state, en: state) {
        state.english = en;
    },
    setArabic(state: state, ar: state) {
        state.arabic = ar;
    }
}

const actions = {
    setLang(context: ActionContext<state, state>, lang: string) {
        context.commit('setLang', lang);
    },
    setEnglish(context: ActionContext<state, state>, en: state) {
        context.commit('setEnglish', en);
    },
    setArabic(context: ActionContext<state, state>, ar: state) {
        context.commit('setArabic', ar);
    }
}

const getters = {
    Language: (state: state) => {
        return (state.lang == 'en') ? state.english : state.arabic;
    }
}

export default {
    state,
    mutations,
    actions,
    getters
}
   