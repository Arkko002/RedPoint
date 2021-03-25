<template>
  <div class="app">
	<router-view></router-view>		
  </div>
</template>

<script>

export default {
	name: "app",
	components: {}
};
</script>

<style>
:root {
	/*TODO Proper color scheme and UX design*/
	--main-bg-color: #36393f;
	--alt-bg-color: #65686f;
	--highlight-color: #820a04;
	--alt-highlight-color: #20368f`;
}

* {
	box-sizing: border-box;
}

.app {
	height: 100vh;
	font-family: Avenir, Helvetica, Arial, sans-serif;
	-webkit-font-smoothing: antialiased;
	-moz-osx-font-smoothing: grayscale;
	overflow: auto;
	background-color: var(--main-bg-color);
}
</style>
