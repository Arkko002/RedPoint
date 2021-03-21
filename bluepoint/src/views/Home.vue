<template>
  <div class="home">
		<TheNavbar class="home__navbar"/>

		<div class="component-container">
			<router-view></router-view>
		</div>
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
.home {
	position: absolute;
	inset: 0 0 0 0;
	display: block;
	overflow: auto;
	background-color: var(--main-bg-color);

}

.component-container {
	height: 100%;
	display: flex;
	flex-direction: column;
	align-items: center;
	justify-content: center;
}

</style>
