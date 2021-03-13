<template>
  <div class="home">
		<TheNavbar/>
		<router-view></router-view>
  </div>
</template>

<script>
import TheNavbar from "@/components/TheNavbar.vue";

/**
 * Component used as a router view for main landing page.
 */
export default {
	name: "Home",
	components: {
		TheNavbar
	},
};
</script>

<style>
.home > * {
	display: flex;
	justify-content: center;
}
</style>
