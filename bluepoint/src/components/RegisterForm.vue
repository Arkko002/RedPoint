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
		requiredFieldsFilled() {
			if (!this.userName || !this.password) return false;
			if (this.password != this.passwordConfirmation) return false;

			return true;
		},

		registering() {
			return this.$store.state.authentication.status.registering;
		}
	},

	methods: {
		submitForm: function(e) {
			this.$storestore.dispatch("authentication/register", {
				username,
				password,
				email
			});
		}
	}
};
</script>
