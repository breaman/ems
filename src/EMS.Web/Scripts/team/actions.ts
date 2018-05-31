import { ActionTree } from 'vuex';
import { TeamState, TEAMS_LOADED, TEAMS_ERROR, Team, TEAM_CREATE } from './types';
import { RootState } from '../types';
import axios from 'axios';

export const actions: ActionTree<TeamState, RootState> = {
    fetchTeams({ commit }): any {
        axios.get('/api/teams')
        .then((response) => {
            const payload: Array<Team> = response && response.data;
            commit(TEAMS_LOADED, payload);
        }, (error) => {
            console.log(error);
            commit(TEAMS_ERROR);
        });
    },

    addNewTeam({commit}): any {
        axios.post('/api/team/add')
        .then((response) => {
            const payload: Array<Team> = response && response.data;
            commit(TEAM_CREATE, payload);
        }, (error) => {
            console.log(error);
            commit(TEAMS_ERROR);
        });
    }
};