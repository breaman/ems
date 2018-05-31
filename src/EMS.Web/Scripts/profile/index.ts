import { Module } from 'vuex';
import { RootState } from '../types';
import { ProfileState } from './types';
import { getters } from './getters';
import { actions } from './actions';
import { mutations } from './mutations';

export const state: ProfileState = {
    user: undefined,
    error: false
};

const namespaced: boolean = true;

export const profile: Module<ProfileState, RootState> = {
    namespaced,
    state,
    getters,
    actions,
    mutations
};