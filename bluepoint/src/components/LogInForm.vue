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
export default {
	name: "LogInForm",

	data() {
		return {
			username: { type: String },
			password: { type: String },
		};
	},

	computed: {
		loggingIn() {
			return this.$store.state.authentication.status.loggingIn;
		},
	},

	methods: {
		requiredFieldsFilled() {
			if (!this.name) {
				this.errors.push("Name required.");
			}
			if (!this.age) {
				this.errors.push("Age required.");
			}

			return this.userName && this.password;
		},

		submitForm() {
			const username = this.username;
			const password = this.password;

			this.$store.dispatch("athentication/login", { username, password });
		},
	},
};
</script>
