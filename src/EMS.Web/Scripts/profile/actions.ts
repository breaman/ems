import { ActionTree } from 'vuex';
import axios from 'axios';
import { ProfileState, User } from './types';
import { RootState } from '../types';
import { PROFILE_LOADED, PROFILE_ERROR } from './mutations';


export const actions: ActionTree<ProfileState, RootState> = {
    fetchData({ commit }): any {
        axios({
            url: 'https://localhost:5001/api/user'
        }).then((response) => {
            const payload: User = response && response.data;
            commit(PROFILE_LOADED, payload);
        }, (error) => {
            console.log(error);
            commit(PROFILE_ERROR);
        });
    }
};