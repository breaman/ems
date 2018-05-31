import { MutationTree } from 'vuex';
import { ProfileState, User } from './types';

export const PROFILE_LOADED = 'profileLoaded';
export const PROFILE_ERROR = 'profileError';

export const mutations: MutationTree<ProfileState> = {
    [PROFILE_LOADED](state, payload: User) {
        state.error = false;
        state.user = payload;
    },
    [PROFILE_ERROR](state) {
        state.error = true;
        state.user = undefined;
    }
};