<template>
  <div class="register-container">
    <TheNavbar />
    <RegisterForm />
  </div>
</template>

<script>
import TheNavbar from "@/components/TheNavbar";
import RegisterForm from "@/components/RegisterForm.vue";

export default {
  name: "Register",
  components: {
    TheNavbar,
    RegisterForm
  }
};
</script>
