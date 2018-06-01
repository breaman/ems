import Vue from 'vue';
import axios from 'axios';
import VeeValidate, { Validator } from 'vee-validate';
import toastr from 'toastr';
import { CreditCardInfo, CheckoutViewModel, TeamCheckoutInfo } from './Interfaces';

Vue.use(VeeValidate);

var checkoutVm = new Vue({
    el: '#checkout',
    data: {
        teams: [],
        ccInfo: null,
        agreesToTerms: false,
        success: true,
        hasPaymentEntries: false,
        useDefaultAddress: true,
        validationErrors: [],
        processing: false,
        paymentSuccessful: false
    },
    created() {
        axios.get('/api/checkout').then((result) => {
            this.teams = result.data.teams;
            this.ccInfo = result.data.ccInfo;
            this.agreesToTerms = result.data.agreesToTerms;
            this.success = result.data.success;
            if (this.teams.length > 0) {
                this.hasPaymentEntries = true;
            }
        });
    },
    methods: {
        remove(index: number) {
            this.teams.splice(index, 1);
        },
        async submitPayment() {
            var isValid = true;
            if (!this.processing) {
                this.processing = true;
                try {
                    isValid = await this.$validator.validateAll();
                }
                catch (error) {
                    isValid = false;
                }
                if (isValid) {
                    this.validationErrors = [];
                    axios.post('/api/checkout', { teams: this.teams, ccInfo: this.ccInfo, agreesToTerms: this.agreesToTerms, useDefaultAddress: this.useDefaultAddress })
                        .then((result) => {
                            this.processing = false;
                            if (result.data.errors.length == 0) {
                                this.validationErrors = [];
                                this.teams = [];
                                this.paymentSuccessful = true;
                                toastr.success('Payment succeeded');
                            }
                            else {
                                this.validationErrors = result.data.errors;
                                toastr.error('An error occurred while processing your request. Please correct the errors and try again.');
                            }
                        })
                        .catch((error) => {
                            this.processing = false;
                            toastr.error('Not able to process your payment at this time.');
                        })
                }
                else {
                    this.processing = false;
                    toastr.error('Your payment information is not valid, please correct the errors and try again.');
                }
            }
        }
    },
    computed: {
        totalCost() {
            let total = 0;
            for (var i = 0; i < this.teams.length; i++) {
                total += this.teams[0].cost;
            }
            return total;
        }
    }
} as Vue.ComponentOptions<CheckoutViewModel>)