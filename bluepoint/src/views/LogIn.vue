<template>
  <div class="sign-in-container">
    <TheNavbar />
    <LogInFrom />
  </div>
</template>

<script>
import TheNavbar from "@/components/TheNavbar.vue";
import LogInForm from "@/components/LogInForm.vue";

export default {
  name: "LogIn",
  components: {
    TheNavbar,
    LogInForm
  }
};
</script>
