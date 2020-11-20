<template>
	<div class="sign-in-container">
		<ErrorPopup/>
		<LogInForm/>
	</div>
</template>

<script>
import LogInForm from "@/components/LogInForm.vue";
import ErrorPopup from "@/components/ErrorPopup";

/**
 * Component used as a router view for account log in.
 * For login form details see [LogInForm]{@link LogInForm}.
 */
export default {
	name: "LogIn",
	components: {
		LogInForm,
		ErrorPopup
	}
};
</script>
