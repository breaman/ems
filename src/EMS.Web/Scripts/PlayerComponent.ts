import Vue from 'vue';
import { PlayerViewModel } from './Interfaces';
import { Utility } from './Utils';

Vue.component('player', {
    template: '#player-template',
    props: ['player', 'index'],
    data() {
        return {
            genders: [],
            shirtSizes: [],
            grades: [],
            experiences: [],
            frequencies: [],
            feet: [],
            inches: [],
            countryRadioName: `country_${this.index}`,
            showModal: false,
            displayPlayerDetails: !(this.player.id != null && this.player.id != 0),
            //statesProvinces: []
        }
    },
    created() {
        this.genders = [{ id: 'M', name: 'Male' }, { id: 'F', name: 'Female' }];
        this.shirtSizes = [{ id: 'YL', name: 'Youth Large' }, { id: 'S', name: 'Adult Small' }, { id: 'M', name: 'Adult Medium' }, { id: 'L', name: 'Adult Large' }, { id: 'XL', name: 'Adult XL' }, { id: 'XXL', name: 'Adult XXL' }, { id: 'XXXL', name: 'Adult XXXL' }];
        this.grades = [{ id: -1, name: 'N/A' }, { id: 1, name: '1st' }, { id: 2, name: '2nd' }, { id: 3, name: '3rd' }, { id: 4, name: '4th' }, { id: 5, name: '5th' }, { id: 6, name: '6th' }, { id: 7, name: '7th' }, { id: 8, name: '8th' }, { id: 9, name: '9th' }, { id: 10, name: '10th' }, { id: 11, name: '11th' }, { id: 12, name: '12th' }];
        this.experiences = [{ id: 1, name: 'No organized basketball experience' }, { id: 2, name: 'YMCA, YWCA or grade school' },
        { id: 3, name: 'Other youth program' }, { id: 4, name: 'AAU' },
        { id: 5, name: 'Junior high/middle school' }, { id: 6, name: 'High school freshman' },
        { id: 7, name: 'High school junior varsity' }, { id: 8, name: 'Varsity high school (<500 students)' },
        { id: 9, name: 'Varsity high school (>500 students)' }, { id: 10, name: 'Adult league or college intermurals' },
        { id: 11, name: 'College' }, { id: 12, name: 'Professional' }];
        this.frequencies = [{ id: 1, name: 'None (<5 times)' }, { id: 2, name: 'Some (5-25 times)' }, { id: 3, name: 'Lots (>25 times)' }];
        this.feet = [{ id: 3, name: '3' }, { id: 4, name: '4' }, { id: 5, name: '5' }, { id: 6, name: '6' }, { id: 7, name: '7' }];
        this.inches = [{ id: 0, name: '0' }, { id: 1, name: '1' }, { id: 2, name: '2' }, { id: 3, name: '3' }, { id: 4, name: '4' }, { id: 5, name: '5' }, { id: 6, name: '6' }, { id: 7, name: '7' }, { id: 8, name: '8' }, { id: 9, name: '9' }, { id: 10, name: '10' }, { id: 11, name: '11' }];
    },
    methods: {
        deletePlayer() {
            this.showModal = true;
            //alert(`Are you sure you want to delete ${this.fullName}?`);
        }
    },
    computed: {
        fullName() {
            if (this.player.firstName !== '' || this.player.lastName !== '') {
                return `${this.player.firstName} ${this.player.lastName}`;
            }
            else {
                return 'Unnamed Player';
            }
        },
        playerType() {
            if (this.index == 0) {
                return 'Captain';
            }
            else {
                return `Player# ${this.index + 1}`;
            }
        },
        statesProvinces() {
            let returnValue = [];

            if (this.player.country == "US")
                returnValue = Utility.states();
            else {
                returnValue = Utility.provinces();
            }

            if (returnValue.indexOf(this.player.state) === -1) {
                this.player.state = null;
            }

            return returnValue;
        },
        genderDisplayName() {
            let gender = '';

            if (this.player.gender == "M")
                gender = 'Male';
            if (this.player.gender == "F")
                gender = 'Female';

            return gender;
        },
        heightDisplay() {
            if (this.player.heightFeet !== null && this.player.heightInches !== null)
                return `${this.player.heightFeet}'${this.player.heightInches}"`;
        }
    }
} as Vue.ComponentOptions<PlayerViewModel>);