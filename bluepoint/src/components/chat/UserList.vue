<template>
	<div class="user-list">
		<div v-if="users">
		<li v-for="user in users" :key="user.id">
			<button class="user-list__button">
				<img class="user-list__avatar" :src="user.image" />
				<label class="user-list__name-label" :content="user.name" />
			</button>
		</li>
	</div>
	</div>
</template>

<script>
import { HubConnection } from "signalr";

/**
 * Display of server's users, and user list related actions.
 */
export default {
	//TODO
	name: "UserList",
	props: {
		users: Array,
		connection: HubConnection
	},
};
</script>
