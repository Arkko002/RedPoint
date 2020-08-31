<template>
	<nav class="navbar">
		<router-link :to="{ name: 'home' }">BluePoint</router-link>
		<ul v-if="!loggedIn">
			<li class="nav-item nav-item-auth">
				<router-link class="nav-link" :to="{ name: 'register' }">Sign Up</router-link>
			</li>
			<li class="nav-item nav-item-auth">
				<router-link class="nav-link" :to="{ name: 'login' }">Sign In</router-link>
			</li>
			<li class="nav-item">
				<router-link class="nav-link" :to="{ name: 'about' }">About</router-link>
			</li>
		</ul>
		<ul v-else>
			<li class="nav-item">
				<router-link class="nav-link" :to="{ name: 'chat' }">Chat</router-link>
			</li>
			<li class="nav-item nav-item-auth">
				<router-link class="nav-link" :to="{ name: 'logout' }">Log Out</router-link>
			</li>
			<li class="nav-item">
				<router-link class="nav-link" :to="{ name: 'about' }">About</router-link>
			</li>
		</ul>
	</nav>
</template>

<script>
export default {
	name: "TheNavbar",
	computed: {
		loggedIn() {
			return this.$store.state.loggedIn;
		}
	}
};
</script>

// TODO
<style></style>
