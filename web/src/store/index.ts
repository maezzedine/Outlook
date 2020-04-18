import Vue from 'vue';
import Vuex from 'vuex';
import language from './modules/language';

Vue.use(Vuex);

const debug = process.env.NODE_ENV !== 'production'

export default new Vuex.Store({
    modules: {
        language
    },
    strict: debug
})