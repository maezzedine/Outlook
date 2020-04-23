import Vue from 'vue';
import Vuex from 'vuex';
import language from './modules/language';
import colors from './modules/colors';
import authentication from './modules/authentication';

Vue.use(Vuex);

const debug = process.env.NODE_ENV !== 'production'

export default new Vuex.Store({
    modules: {
        language,
        colors,
        authentication
    },
    strict: debug
})