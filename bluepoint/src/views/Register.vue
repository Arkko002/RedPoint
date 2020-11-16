<template>
  <div class="register-container">
    <RegisterForm />
  </div>
</template>

<script>
import RegisterForm from "@/components/RegisterForm.vue";

/**
 * Component used as a router view for user's account registration.
 * For registration form details see [RegisterForm]{@link RegisterForm}.
 */
export default {
	name: "Register",
	components: {
		RegisterForm
	}
};
</script>
