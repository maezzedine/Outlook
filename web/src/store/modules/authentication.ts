import { ActionContext } from 'vuex';
import outlookUser from '../../models/outlookUser';
import state from '../state';
import { api } from '../../services/api';
import { authService } from '../../services/auth-service';

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
        state.user.expirayDate = new Date();
    },
    setUsername(state: state, username: string) {
        state.user.username = username;
    }
}

const actions = {
    setUser(context: ActionContext<state, state>, user: outlookUser) {
        context.commit('setUser', user);
    },
    removeUser(context: ActionContext<state, state>) {
        context.commit('removeUser');
    },
    setUsername(context: ActionContext<state, state>, username: string) {
        context.commit('setUsername', username);
    }
}

const getters = {
    User: (state: state) => {
        return state.user;
    },
    IsAuthenticated: (state: state) => {
        return state.user.token != '';
    },
    Username: (state: state) => {
        return state.user.username;
    },
    IsAubStaff: async (state: state) => {
        var aubDomains = ['mail.aub.edu', 'aub.edu.lb'];
        if (state.user.token == '') return false;

        var email = '';
        await authService.getUser().then(d => {
            email = d['email'];
        })

        return aubDomains.includes(email.split('@')[1])
    }
}

export default {
    state,
    mutations,
    actions,
    getters
}