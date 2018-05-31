import Vue from 'vue';
import Vuex, {StoreOptions} from 'vuex';
import {RootState} from './types';
import {profile} from './profile';
import {team} from './team';

Vue.use(Vuex);

const store: StoreOptions<RootState> = {
    state: {
        version: '1.0.0'
    },
    modules: {
        profile: profile,
        team: team
    }
};

export default new Vuex.Store<RootState>(store);