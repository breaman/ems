import { MutationTree } from 'vuex';
import { TeamState, Team, TEAMS_ERROR, TEAMS_LOADED, TEAM_CREATE } from './types';

export const mutations: MutationTree<TeamState> = {
    [TEAMS_LOADED](state, payload: Array<Team>) {
        state.error = false;
        state.teams = payload;
    },
    [TEAMS_ERROR](state) {
        state.error = true;
        state.teams = undefined;
    },
    [TEAM_CREATE](state, payload: Team) {
        state.error = false;
        state.teams = [...state.teams!, payload];
    }
};