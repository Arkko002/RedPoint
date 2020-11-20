<template>
  <form id="registerForm" @submit="submitForm">
    <p>
      <label for="userNameInput">User Name</label>
      <input id="userNameInput" type="text" v-model="username" />
    </p>

    <p>
      <label for="passwordInput">Password</label>
      <input id="passwordInput" type="text" v-model="password" />
    </p>

    <p>
      <label for="passwordConfirmationInput">Password Confirmation</label>
      <input
        id="passwordConfirmationInput"
        type="text"
        v-model="passwordConfirmation"
      />
    </p>
    <p>
      <label for="emailInput">E-mail</label>
      <input id="emailInput" type="text" v-model="email" />
    </p>

    <p>
      <input
        v-bind:disabled="!requiredFieldsFilled || registering"
        type="submit"
        value="Submit"
      />
    </p>
  </form>
</template>

<script>
/**
 * Contains logic related to creation of user's account.
 */
export default {
	name: "RegisterForm",
	data() {
		return {
			username: { type: String },
			password: { type: String },
			passwordConfirmation: { type: String },
			email: { type: String }
		};
	},

	computed: {
		/**
     * Checks if all of the fields necessary for account creation were filled.
     * @returns {boolean}
     */
		requiredFieldsFilled() {
			if (!this.username || !this.password) return false;
			return this.password === this.passwordConfirmation;
		},

		/**
     * Checks if any registration process is already started by the current session.
     * @returns {any}
     */
		registering() {
			return this.$store.state.authentication.status.registering;
		},
		
		alert(){
			return this.$store.state.alert.type;
		}
	},

	methods: {
		/**
     * Submits the form data to Vuex authentication module.
     */
		submitForm() {
			const username = this.username;
			const password = this.password;
			const email = this.email;
          
			this.$store.dispatch("authentication/register", {
				username,
				password,
				email
			});
		}
	}
};
</script>
