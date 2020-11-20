<template>
  <div class="register-container">
  	<ErrorPopup />
		<RegisterForm />
  </div>
</template>

<script>
import RegisterForm from "@/components/RegisterForm.vue";
import ErrorPopup from "@/components/ErrorPopup";

/**
 * Component used as a router view for user's account registration.
 * For registration form details see [RegisterForm]{@link RegisterForm}.
 */
export default {
	name: "Register",
	components: {
		RegisterForm,
		ErrorPopup
	}
};
</script>
