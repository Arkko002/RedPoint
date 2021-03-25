<template>
  <div class="home">
		<TheNavbar class="home__navbar"/>

		<div class="home__component-container">
			<router-view></router-view>
		</div>

		<!--TODO Footer with stuff, possibly legal-->
		<footer> </footer>
  </div>
</template>

<script>
import TheNavbar from "@/components/home/TheNavbar.vue";

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
.home {
	display: flex;
	flex-direction: column;
	height: 100%;
}

.home__component-container {
	height: 100%;
	overflow: auto;
	display: flex;
	flex-direction: column;
	align-items: center;
	justify-content: center;
}

.home__navbar {
	flex-grow: 2;
}

</style>
