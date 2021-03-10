<template>
	<form class="login-form">
		<p>
			<label for="name-input">User Name</label>
			<input id="name-input" type="text" v-model="username" />
		</p>

		<p>
			<label for="password-input">Password</label>
			<input id="password-input" type="password" v-model="password" />
		</p>

		<p>
			<input
				v-bind:disabled="!requiredFieldsFilled || loggingIn"
				v-on:click="submitForm"
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
			username: "",
			password: "",
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
			//TODO
			// if (this.username.length === 0) {
			// 	this.errors.push("Username required.");
			// }
			// if (!this.password.length === 0) {
			// 	this.errors.push("Password required.");
			// }

			return Boolean(this.username) && Boolean(this.password);
		},

		/**
     * Submits the form data to Vuex authentication module.
     */
		submitForm() {
			const username = this.username.toString();
			const password = this.password.toString();

			this.$store.dispatch("authentication/login", { username, password });
		},
	},
};
</script>

<style>
.login-form {
	width: 200px;
	display: flex;
	flex-direction: column;
	align-items: center;
	justify-content: center;
}
</style>