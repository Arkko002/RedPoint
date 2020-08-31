<template>
  <div class="sign-in-container">
    <LogInForm />
  </div>
</template>

<script>
import LogInForm from "@/components/LogInForm.vue";

export default {
	name: "LogIn",
	components: {
		LogInForm
	}
};
</script>
