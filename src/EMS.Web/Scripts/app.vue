<template>
    <div id="app">
        <h1>{{ title }}</h1>
        <p>{{ description }}</p>
        <div>Hello world</div>
    </div>
</template>

<script lang="ts">
import Vue from 'vue';
import Component from 'vue-class-component';

@Component({
    props: {
        description: String
    }
})
export default class App extends Vue {
    title: string;

    constructor() {
        super();
        this.title = 'This is just a quick title';
        // this.description = 'Here is just a simple description to see how things work';
    }
}
</script>