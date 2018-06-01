import ES6Promise from 'es6-promise';
import Vue from 'vue';

ES6Promise.polyfill();

if (Array.prototype.find == null) {
    Array.prototype.find = function (callback, thisArg) {
        for (var i = 0; i < this.length; i++) {
            if (callback.call(thisArg || window, this[i], i, this))
                return this[i];
        }
        return undefined;
    };
}

if (typeof Object.assign != 'function') {
    Object.assign = function (target: any, varArgs: any) { // .length of function is 2
        'use strict';
        if (target == null) { // TypeError if undefined or null
            throw new TypeError('Cannot convert undefined or null to object');
        }

        var to = Object(target);

        for (var index = 1; index < arguments.length; index++) {
            var nextSource = arguments[index];

            if (nextSource != null) { // Skip over if undefined or null
                for (var nextKey in nextSource) {
                    // Avoid bugs when hasOwnProperty is shadowed
                    if (Object.prototype.hasOwnProperty.call(nextSource, nextKey)) {
                        to[nextKey] = nextSource[nextKey];
                    }
                }
            }
        }
        return to;
    };
}

export class Utility {
    static states() {
        return ['AK', 'AL', 'AR', 'AZ', 'CA', 'CO', 'CT', 'DC', 'DE', 'FL',
            'GA', 'HI', 'IA', 'ID', 'IL', 'IN', 'KS', 'KY', 'LA', 'MA',
            'MD', 'ME', 'MI', 'MO', 'MN', 'MS', 'MT', 'NC', 'ND', 'NE',
            'NH', 'NJ', 'NM', 'NV', 'NY', 'OH', 'OK', 'OR', 'PA', 'RI',
            'SC', 'SD', 'TN', 'TX', 'UT', 'VT', 'VA', 'WA', 'WI', 'WV',
            'WY'];
    }
    static provinces() {
        return ['AB', 'BC', 'MB', 'NB', 'NF', 'NS', 'NT', 'ON', 'PE', 'PQ',
            'SK', 'YT'];
    }
}

Vue.component('modal', {
    template: '#delete-template'
});

Vue.component('action-modal', {
    template: '#action-template'
})