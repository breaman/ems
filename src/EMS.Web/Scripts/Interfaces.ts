import Vue from 'vue';

export interface TeamsViewModel extends Vue {
    teams: Array<Team>;
    errors: Array<string>;
    teamsReadyForCheckout: boolean;
    addTeam() : void;
}

export interface CheckoutViewModel extends Vue {
    teams: Array<TeamCheckoutInfo>;
    ccInfo: CreditCardInfo;
    agreesToTerms: boolean;
    success: boolean;
    totalCost: number;
    hasPaymentEntries: boolean;
    useDefaultAddress: boolean;
    validationErrors: Array<string>;
    processing: boolean;
    paymentSuccessful: boolean;
}

export interface TeamViewModel extends Vue {
    team: Team;
    status: string;
    validationErrors: Array<string>;
    teamDisplayName: string;
    divisions: Array<Division>;
    genders: Array<Gender>;
    competitionLevels: Array<CompetitionLevel>;
    displayPromoCode: boolean;
    showActionModal: boolean;
    hasPromoCode: boolean;
    initializeDropDowns(): void;
    removeTeam(): void;
}

export interface PlayerViewModel extends Vue {
    player: Player;
    fullName: string;
    index: number;
    fields: any;
    showModal: boolean;
    genders: Array<GenderString>;
    shirtSizes: Array<ShirtSize>;
    grades: Array<Grade>;
    experiences: Array<PlayingExperience>;
    frequencies: Array<PlayingFrequency>;
    feet: Array<HeightFeet>;
    inches: Array<HeightInches>;
    statesProvinces: Array<string>;
    genderDisplayName: string;
    initializeDropDowns(): void;
    removePlayer(): void;
}

export class Division {
    id: number;
    name: string;
}

export class Gender {
    id: number;
    name: string;
}

export class GenderString {
    id: string;
    name: string;
}

export class CompetitionLevel {
    id: number;
    name: string;
}

export class Grade {
    id: number;
    name: string;
    constructor(id: number, name: string) {
        this.id = id;
        this.name = name;
    }
}

export class PlayingExperience {
    id: number;
    name: string;
    constructor(id: number, name: string) {
        this.id = id;
        this.name = name;
    }
}

export class PlayingFrequency {
    id: number;
    name: string;
    constructor(id: number, name: string) {
        this.id = id;
        this.name = name;
    }
}

export class ShirtSize {
    id: string;
    name: string;
    constructor(id: string, name: string) {
        this.id = id;
        this.name = name;
    }
}

export class HeightFeet {
    id: number;
    name: string;
    constructor(id: number, name: string) {
        this.id = id;
        this.name = name;
    }
}

export class HeightInches {
    id: number;
    name: string;
    constructor(id: number, name: string) {
        this.id = id;
        this.name = name;
    }
}

export class StateProvince {
    id: string;
    name: string;
}

export class TeamCheckoutInfo {
    teamId: number;
    name: string;
    division: string;
    promoCode: string;
    playerCount: number;
    cost: number;
}

export class CreditCardInfo {
    firstName: string;
    lastName: string;
    address: string;
    city: string;
    state: string;
    countryCode: string;
    postalCode: string;
    cardNumber: string;
    month: number;
    year: number;
    cvv: string;
}

export class Team {
    id: number;
    name: string;
    division: Division;
    gender: Gender;
    competitionLevel: CompetitionLevel;
    promotionalCode: string;
    players: Array<Player>;
    status: string;
    validationErrors: Array<string>;
}

export class Player {
    id: number;
    firstName: string;
    lastName: string;
    gender: string;
    age: number;
    nextGrade: number;
    heightFeet: number;
    heightInches: number;
    address: string;
    city: string;
    state: string;
    country: string;
    zip: string;
    phone: string;
    birthdate: string;
    email: string;
    shirtSize: string;
    playingExperience: number;
    playingFrequency: number;
    playedLastYear: boolean;
}