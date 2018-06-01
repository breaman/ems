import Vue from 'vue';
import axios from 'axios';
import toastr from 'toastr';
import { TeamViewModel, Player, GenderString } from './Interfaces';

Vue.component('team', {
    template: '#team-template',
    props: ['team'],
    data() {
        return {
            validationErrors: [],
            genders: [],
            divisions: [],
            competitionLevels: [],
            displayTeamDetails: !(this.team.id != null && this.team.id != 0),
            displayPromoCode: false,
            showActionModal: false,
            hasPromoCode: false
        }
    },
    computed: {
        teamDisplayName() {
            if (this.team.name === '') {
                return 'Unnamed Team';
            }
            else {
                return this.team.name;
            }
        }
    },
    created() {
        if (this.team.players === null) {
            this.team.players = new Array<Player>();
        }

        this.initializeDropDowns();
        this.hasPromoCode = this.team.promotionalCode != null && this.team.promotionalCode != '';
    },
    watch: {
        'team.division': function () {
            if (this.team.division != null && this.team.division.id != null) {
                axios.get('/api/genders/' + this.team.division.id).then((result) => { this.genders = result.data; });
            }
        },
        'team.gender': function () {
            if (this.team.division != null && this.team.division.id != null && this.team.gender != null && this.team.gender.id != null) {
                axios.get(`/api/competitionLevels/${this.team.division.id}/${this.team.gender.id}`).then((result) => { this.competitionLevels = result.data; });
            }
        }
    },
    methods: {
        initializeDropDowns() {
            axios.get('/api/divisions').then((result) => {
                this.divisions = result.data;
                if (this.team.division != null && this.team.division.id != null) {
                    this.team.division = this.divisions.find(d => d.id == this.team.division.id);
                }
            });

            if (this.team.division != null && this.team.division.id != null) {
                axios.get('/api/genders/' + this.team.division.id).then((result) => {
                    this.genders = result.data;
                    if (this.team.gender != null && this.team.gender.id != null) {
                        this.team.gender = this.genders.find(g => g.id == this.team.gender.id);
                    }
                });
            }

            if (this.team.division != null && this.team.division.id != null && this.team.gender != null && this.team.gender.id != null) {
                axios.get(`/api/competitionLevels/${this.team.division.id}/${this.team.gender.id}`).then((result) => {
                    this.competitionLevels = result.data;
                    if (this.team.competitionLevel != null && this.team.competitionLevel.id != null) {
                        this.team.competitionLevel = this.competitionLevels.find(cl => cl.id == this.team.competitionLevel.id);
                    }
                });
            }
        },
        addPlayer() {
            var player = new Player();
            player.firstName = '';
            player.lastName = '';
            player.gender = null;
            player.age = null;
            player.nextGrade = null;
            player.heightFeet = null;
            player.heightInches = null;
            player.country = "US";
            this.team.players.push(player);
        },
        cancel() {
            axios.get(`/api/team/${this.team.id}`, {
                headers: { 'cache-control': 'no-store'}
            })
                .then((result) => {
                    Object.assign(this.team, result.data);
                    this.displayPromoCode = false;
                })
                .catch((error) => {
                    alert(error);
                    alert('Something went horribly wrong');
                });
        },
        async save() {
            this.showActionModal = true;
            var isValid = true;
            try {
                isValid = await this.$validator.validateAll();
                for (var i = 0; i < this.$children.length; i++) {
                    isValid = isValid && (await this.$children[i].$validator.validateAll());
                }
            }
            catch (error) {
                isValid = false;
            }
            if (isValid) {
                axios.post('/api/team', this.team)
                    .then((result) => {
                        this.team.id = result.data.id;
                        if (result.data.players != null) {
                            for (var i = 0; i < result.data.players.length; i++) {
                                this.team.players[i].id = result.data.players[i].id;
                            }
                        }
                        this.team.validationErrors = result.data.validationErrors;
                        this.team.status = result.data.status;
                        this.validationErrors = [];
                        this.showActionModal = false;
                        toastr.success(`${this.team.name} successfully saved.`);
                    })
                    .catch((error) => {
                        this.validationErrors = error.response.data.messages;
                        this.showActionModal = false;
                        toastr.error(`Not able to save ${this.team.name}. Please correct the errors and try again`);
                    });
            }
            else {
                this.showActionModal = false;
                toastr.error(`${this.team.name} has errors and is not valid for saving. Please correct these errors and try again.`);
            }
        }
    }
} as Vue.ComponentOptions<TeamViewModel>);
