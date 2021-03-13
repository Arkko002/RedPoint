<template>
	<nav class="navbar">
<!--		TODO Logo-->
		<router-link class="navbar-logo navbar-item" :to="{ name: 'home' }">BluePoint</router-link>
		<div v-if="!loggedIn">
				<router-link class="navbar-item" :to="{ name: 'register' }">Sign Up</router-link>
				<router-link class="navbar-item" :to="{ name: 'login' }">Sign In</router-link>
				<router-link class="navbar-item" :to="{ name: 'about' }">About</router-link>
		</div>
		<div v-else>
				<router-link class="navbar-item" :to="{ name: 'chat' }">Chat</router-link>
				<button class="navbar-item" v-on:click="logOut">Log Out</button>
				<router-link class="navbar-item" :to="{ name: 'about' }">About</router-link>
		</div>
	</nav>
</template>

<script>
export default {
	name: "TheNavbar",
	computed: {
		loggedIn() {
			return this.$store.state.authentication.status.loggedIn;
		}
	},
	methods: {
		logOut() {
			this.$store.dispatch("authentication/logout");
		}
	}
};
</script>

<!--// TODO-->
<style>
.navbar {
	display: flex;
}

.navbar-item {
	flex: 1;
	padding: 1em;
}

.navbar-logo {
  padding: unset;
}

@media all and (max-width: 600px) {
	.navbar{
		flex-wrap: wrap;
	}
	
	.navbar-item {
		flex-basis: 50%;
	}
}

@media all and (max-width: 400px){
	.navbar-item{
		flex-basis: 100%;
	}
}

</style>

