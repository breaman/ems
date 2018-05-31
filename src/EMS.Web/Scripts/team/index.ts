import { Module } from 'vuex';
import { RootState } from '../types';
import { TeamState } from './types';
import { actions } from './actions';
import { mutations } from './mutations';

export const state: TeamState = {
    teams: undefined,
    error: false
};

const namespaced: boolean = true;

export const team: Module<TeamState, RootState> = {
    namespaced,
    state,
    actions,
    mutations
};