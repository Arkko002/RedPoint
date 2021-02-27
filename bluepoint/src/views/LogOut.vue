<script>
import router from "@/router/index";

/**
 * Component used as a router view for account log out.
 */
export default {
	name: "LogOut",
	created() {
		const user = localStorage.getItem("userToken");

		if (user == null) {
			router.push("/");
			return;
		}

		this.$store.dispatch("authentication/logout", user);
		router.push("/");
	}
};
</script>
