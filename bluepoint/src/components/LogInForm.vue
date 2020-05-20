<template>
  <form id="logInForm">
    <p>
      <label for="userNameInput">User Name</label>
      <input id="userNameInput" type="text" v-model="username" />
    </p>

    <p>
      <label for="passwordInput">Password</label>
      <input id="passwordInput" type="text" v-model="password" />
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
			password: { type: String }
		};
	},

	computed: {
		requiredFieldsFilled() {
			return !(!this.userName || !this.password);

      
		},

		loggingIn() {
			return this.$store.state.authentication.status.loggingIn;
		}
	},

	methods: {
		submitForm() {
			const username = this.username;
			const password = this.password;
          
			this.$store.dispatch("athentication/login", { username, password });
		}
	}
};
</script>
