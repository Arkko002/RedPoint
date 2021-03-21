<template>
  <div>
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
	--main-bg-color: #323232;
	--alt-bg-color: #6d6d6d;
	--highlight-color: #c40a0a;
	--alt-highlight-color: #20368f`;
}

* {
	box-sizing: border-box;
}

#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  background-color: var(--main-bg-color);
}
</style>
