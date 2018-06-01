import Vue from 'vue';
import axios from 'axios';
import VeeValidate, { Validator } from 'vee-validate';
import moment from 'moment';
import toastr from 'toastr';
import { TeamsViewModel, Gender, Player, Team, Division, CompetitionLevel } from './Interfaces';

const dictionary = {
    en: {
        attributes: {
            firstName: 'First Name',
            lastName: 'Last Name',
            age: 'Age'
        }
    }
}

Vue.use(VeeValidate, dictionary);

//Validator.updateDictionary(dictionary);

var teamManagementVm = new Vue({
    el: '#teams',
    data: {
        teams: []
    },
    mounted() {
        axios.get('/api/teams').then((result) => { this.teams = result.data; });
    },
    computed: {
        teamsReadyForCheckout() {
            let theReturn = false;

            for (var i = 0; i < this.teams.length; i++) {
                if (this.teams[i].status == "Ready For Payment") {
                    theReturn = true;
                }
            }

            return theReturn;
        }
    },
    methods: {
        addTeam() {
            var newTeam = new Team();
            newTeam.name = '';
            newTeam.gender = new Gender();
            newTeam.division = new Division();
            newTeam.competitionLevel = new CompetitionLevel();
            newTeam.players = new Array<Player>();
            newTeam.status = 'Not Ready';
            newTeam.validationErrors = new Array<string>();
            this.teams.push(newTeam);
        }
    }
} as Vue.ComponentOptions<TeamsViewModel>)