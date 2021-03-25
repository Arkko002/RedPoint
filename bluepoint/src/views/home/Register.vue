<template>
	<div class="register">
		<form class="register__form" @submit="submitForm">

			<div class="form__item-container">
				<label class="form__item form__item--label" for="username">User Name</label>
				<input class="form__item form__item--input" type="text" v-model="username" id="username"/>
			</div>

			<div class="form__item-container">
				<label class="form__item form__item--label" for="password">Password</label>
				<input class="form__item form__item--input" type="password" v-model="password" id="password"/>
			</div>

			<div class="form__item-container">
				<label class="form__item form__item--label" for="passwordConfirmation">Password Confirmation</label>
				<input class="form__item form__item--input" type="password" v-model="passwordConfirmation" id="passwordConfirmation"/>
			</div>

			<div class="form__item-container">
				<label class="form__item form__item--label" for="email">E-mail</label>
				<input class="form__item form__item--input" type="email" v-model="email" id="email" />
			</div>

			<div class="form__item-container">
				<input class="form__item form__item--submit"
				v-bind:disabled="!requiredFieldsFilled || registering"
				type="submit"
				value="Submit"/>
			</div>
		</form>
	</div>
</template>

<script>

/**
 * Component used as a router view for user's account registration.
 * For registration form details see [RegisterForm]{@link RegisterForm}.
 */
export default {
	name: "Register",

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
.register__form {
	display: flex;
	flex-direction: column;
	align-items: center;
	justify-content: center;

	background-color: var(--alt-bg-color);
	border: 1px solid black;
	border-radius: 10px;
	
	box-shadow: 5px 5px 3px 1px;
}

.form__item-container {
	display: flex;
	flex-direction: column;
	padding: 10px;
}
</style>
