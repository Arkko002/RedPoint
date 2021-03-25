<template>
	<div class="login">
		<form class="login__form">
			<div class="form__item-container">
				<label class="form__item form__item--label">User Name</label>
				<input class="form__item form__item--input" type="text" v-model="username" />
			</div>

			<div class="form__item-container">
				<label class="form__item form__item--label">Password</label>
				<input class="form__item form__item--input" type="password" v-model="password" />

			</div>
			<div class="form__item-container">
				<input class="form__item form__item--submit"
					v-bind:disabled="!requiredFieldsFilled || loggingIn"
					v-on:click="submitForm"
					type="submit"
					value="Submit"
				/>
			</div>
		</form>
	</div>
</template>

<script>

/**
 * Component used as a router view for account log in.
 * For login form details see [LogInForm]{@link LogInForm}.
 */
export default {
	name: "LogIn",

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
.login__form {
	display: flex;
	flex-direction: column;
	align-items: center;
	justify-content: center;

	background-color: var(--alt-bg-color);
	border: 1px solid black;
	border-radius: 10px;

	box-shadow: 5px 5px 3px 1px;
}

/*TODO CSS duplication between forms, find better place for this css*/

.form__item-container {
	display: flex;
	flex-direction: column;
	padding: 10px;
}

.form__item--label {
	text-align: center;
}
</style>
