<script>
import router from "@/router/index"

export default {
  name: "LogOut",
  created() {
      const user = localStorage.getItem("user");

      if (user == null) {
      router.push("/");
      return;
    }

    this.$store.dispatch("authentication/logout", user);
    router.push("/");
  }
};
</script>
