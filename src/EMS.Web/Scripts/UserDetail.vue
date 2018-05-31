<template>
    <div class="container">
        <div v-if="profile.user">
            <p>
                Full name: {{ fullName }}
            </p>
            <p>
                Email: {{ email }}
            </p>
            <p>
                First Name: {{ firstName }}
            </p>
        </div>
        <div v-if="profile.error">
            Oops an error occured
        </div>
    </div>
</template>

<script lang="ts">
    import Vue from 'vue';
    import { State, Action, Getter } from 'vuex-class';
    import Component from 'vue-class-component';
    import { ProfileState, User } from './profile/types';
    const namespace: string = 'profile';
    
@Component
    export default class UserDetail extends Vue {
        @State('profile') profile: ProfileState | undefined;
        @Action('fetchData', { namespace }) fetchData: any;
        @Getter('fullName', { namespace }) fullName: string | undefined;

        mounted() {
            // fetching data as soon as the component's been mounted
            this.fetchData();
        }

        // computed variable based on user's email
        get email() {
            const user = this.profile && this.profile.user;
            return (user && user.email) || '';
        }

        get firstName() {
            const user = this.profile && this.profile.user;
            return (user && user.firstName) || '';
        }
    }
</script>