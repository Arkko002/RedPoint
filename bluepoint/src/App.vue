
<template>
  <div>
    <TheNavbar/>
    <Home/>
  </div>
</template>

<script>
import TheNavbar from "@/components/TheNavbar.vue";
import Home from "@/views/Home.vue";

export default {
	name: "app",
	components: {
		TheNavbar,
		Home
	}
};
</script>

<style>
* {
	box-sizing: border-box;
}

#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
