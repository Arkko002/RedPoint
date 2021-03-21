<template>

	<nav class="navbar" v-if="!loggedIn" >

		<!--Logo is duplicated to maintain the structure of generated HTML, taking logo out-->
		<!--adds complexity to flex CSS-->

		<router-link class="navbar__logo navbar__item" :to="{ name: 'home' }">
			<img class="logo__image" src="../assets/logo-no-text-50px.png" alt="Home" />
		</router-link>
		<router-link class="navbar__item" :to="{ name: 'register' }">Sign Up</router-link>
		<router-link class="navbar__item" :to="{ name: 'login' }">Sign In</router-link>
		<router-link class="navbar__item" :to="{ name: 'about' }">About</router-link>
	</nav>
	<nav class="navbar" v-else>
		<router-link class="navbar__logo navbar__item" :to="{ name: 'home' }">
			<img class="logo__image" src="../assets/logo-no-text-50px.png" alt="Home" />
		</router-link>
		<router-link class="navbar__item" :to="{ name: 'chat' }">Chat</router-link>
		<button class="navbar__item" v-on:click="logOut">Log Out</button>
		<router-link class="navbar__item" :to="{ name: 'about' }">About</router-link>
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

<style>
.navbar {
	background-color: var(--alt-bg-color);
	border-bottom: 1px solid black;

	display: flex;
	flex: 1;
	flex-direction: row;
	justify-content: space-evenly;
	align-items: center;
	
}

.navbar__item {
	padding: 1em;
}

.navbar__item:hover {
	background-color: var(--highlight-color);
}

.navbar__logo {
	margin-right: auto;
	padding: unset;
}

.logo__image {
	object-fit: contain;
	vertical-align: bottom;
}


@media all and (max-width: 600px) {
	.navbar {
		flex-direction: column;
		flex-wrap: wrap;
	}

	.navbar__logo {
		margin-right: unset;
	}
}

@media all and (max-width: 400px){
	/*TODO*/
	.navbar {
		overflow: hidden;
		min-width: 0;
		min-height: 0;
	}
}

</style>

