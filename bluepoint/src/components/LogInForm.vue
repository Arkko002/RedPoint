<template>
	<form id="logInForm">
		<p>
			<label for="userNameInput">User Name</label>
			<input id="userNameInput" type="text" v-bind="username" />
		</p>

		<p>
			<label for="passwordInput">Password</label>
			<input id="passwordInput" type="password" v-bind="password" />
		</p>

		<p>
			<input
				v-bind:disabled="!requiredFieldsFilled || loggingIn"
				type="submit"
				value="Submit"
			/>
		</p>
	</form>
</template>

<script>
/**
 * Contains user's account log in logic.
 */
export default {
	name: "LogInForm",

	data() {
		return {
			username: { type: String },
			password: { type: String },
		};
	},

	computed: {
		/**
     * Checks if any log in process is already started by the current session.
     * @returns {any}
     */
		loggingIn() {
			return this.$store.state.authentication.status.loggingIn;
		},
	},

	methods: {
		/**
     * Checks if all of the fields necessary for logging in were filled.
     * @returns {boolean}
     */
		requiredFieldsFilled() {
			if (!this.name) {
				this.errors.push("Name required.");
			}
			if (!this.age) {
				this.errors.push("Age required.");
			}

			return Boolean(this.username) && Boolean(this.password);
		},

		/**
     * Submits the form data to Vuex authentication module.
     */
		submitForm() {
			const username = this.username;
			const password = this.password;

			this.$store.dispatch("authentication/login", { username, password });
		},
	},
};
</script>
