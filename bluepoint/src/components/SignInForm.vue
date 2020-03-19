<template>
  <form id="signInForm">
    <p>
      <label for="userNameInput">User Name</label>
      <input id="userNameInput" type="text" v-model="userName" />
    </p>

    <p>
      <label for="passwordInput">Password</label>
      <input id="passwordInput" type="text" v-model="password" />
    </p>

    <p>
      <input
        v-bind:disabled="!requiredFieldsFilled"
        type="submit"
        value="Submit"
      />
    </p>
  </form>
</template>

<script>
export default {
  name: "SignInForm",

  data() {
    return {
      userName: { type: String },
      password: { type: String }
    };
  },

  computed: {
    requiredFieldsFilled: function(e) {
      if (!this.userName || !this.password) return false;

      return true;
    }
  },

  methods: {
    submitForm: function(e) {
      //TODO Submit form to API endpoint, validate in back end
      e.preventDefault();
    }
  }
};
</script>
