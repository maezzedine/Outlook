import { ActionContext } from 'vuex';

const state = {
    colors: Object
}

const mutations = {
    setColors(state: state, colors: Object) {
        state.colors = colors;
    }
}

const actions = {
    setColors(context: ActionContext<state, state>, colors: Object) {
        context.commit('setColors', colors);
    }
}

export default {
    state,
    mutations,
    actions
}