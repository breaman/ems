<template>
    <div class="container">
        <div v-if="team.teams">
            <p>
                Count: {{ team.teams.length }}
            </p>
            <div v-for="team in team.teams">
                <TeamUi v-bind:team=team></TeamUi>
            </div>
            <div>
                <button v-on:click="addTeam()">Add Team</button>
            </div>
        </div>
        <div v-if="team.error">
            Oops an error occured
        </div>
    </div>
</template>

<script lang="ts">
    import Vue from 'vue';
    import { State, Action, Getter } from 'vuex-class';
    import { Component } from 'vue-property-decorator';
    import { TeamState, Team } from './types';
    import TeamUi from './team.vue';
    const namespace: string = 'team';
    
@Component({
    components: {
        TeamUi
    }
})
    export default class Teams extends Vue {
        @State('team') team: TeamState | undefined;
        @Action('fetchTeams', { namespace }) fetchTeams: any;
        @Action('addNewTeam', {namespace}) addNewTeam: any;

        mounted() {
            // fetching data as soon as the component's been mounted
            this.fetchTeams();
        }

        // computed variable based on user's email
        get count() {
            const teams = this.team && this.team.teams;
            return (teams && teams.length) || '';
        }

        addTeam() {
            this.addNewTeam();
        }
    }
</script>