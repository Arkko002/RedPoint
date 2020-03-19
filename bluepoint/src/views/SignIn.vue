<template>
  <div class="sign-in-container">
    <TheNavbar />
    <SignInForm />
  </div>
</template>

<script>
import TheNavbar from "@/components/TheNavbar.vue";
import SignInForm from "@/components/SignInForm.vue";

export default {
  name: "SignIn",
  components: {
    TheNavbar,
    SignInForm
  }
};
</script>
