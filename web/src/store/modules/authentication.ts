import { ActionContext } from 'vuex';
import outlookUser from '../../models/outlookUser';

const state = {
    user: new outlookUser()
}

const mutations = {
    setUser(state: state, user: outlookUser) {
        state.user = user;
    },
    removeUser(state: state) {
        state.user.username = '';
        state.user.token = '';
    }
}

const actions = {
    setUser(context: ActionContext<state, state>, user: outlookUser) {
        context.commit('setUser', user);
    },
    removeUser(context: ActionContext<state, state>) {
        context.commit('removeUser');
    }
}

const getters = {
    User: (state: state) => {
        return state.user;
    },
    IsAuthenticated: (state: state) => {
        return state.user.token != '';
    }
}

export default {
    state,
    mutations,
    actions,
    getters
}