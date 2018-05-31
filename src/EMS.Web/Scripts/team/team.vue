<template>
    <div>{{team.name}}</div>
</template>

<script lang="ts">
import Vue from 'vue';
import { Prop, Component } from 'vue-property-decorator';
import { Team } from './types';

@Component
export default class TeamUi extends Vue {
    @Prop() team: Team | undefined;

    constructor() {
        super();
    }
}
</script>