<template>
  <div class="sign-in-container">
    <LogInForm />
  </div>
</template>

<script>
import LogInForm from "@/components/LogInForm.vue";

/**
 * Component used as a router view for account log in.
 * For login form details see [LogInForm]{@link LogInForm}.
 */
export default {
	name: "LogIn",
	components: {
		LogInForm
	}
};
</script>
