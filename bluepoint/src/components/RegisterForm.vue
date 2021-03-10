<template>
  <form class="register-form" @submit="submitForm">
    <p>
      <label for="name-input">User Name</label>
      <input id="name-input" type="text" v-model="username" />
    </p>

    <p>
      <label for="password-input">Password</label>
      <input id="password-input" type="password" v-model="password" />
    </p>

    <p>
      <label for="password-confirmation-input">Password Confirmation</label>
      <input
        id="password-confirmation-input"
        type="password"
        v-model="passwordConfirmation"
      />
    </p>
    <p>
      <label for="email-input">E-mail</label>
      <input id="email-input" type="text" v-model="email" />
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
			username: "",
			password: "",
			passwordConfirmation: "",
			email: "" 
		};
	},

	computed: {
		/**
     * Checks if all of the fields necessary for account creation were filled.
     * @returns {boolean}
     */
		requiredFieldsFilled() {
			if (this.username.length === 0 || this.password.length === 0) return false;
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

<style>
.register-form {
	width: 200px;
	display: flex;
	flex-direction: column;
	flex-wrap: wrap;
	align-items: center;
	justify-content: space-between;
}
</style>