<template>
  <div class="user-container">
	  <div v-if="users">
		<li v-for="user in users" :key="user.id">
			<button class="user-list-button">
				<img class="user-list-avatar" :src="user.image" />
				<label class="user-list-name" :content="user.name" />
			</button>
		</li>
	  </div>
  </div>
</template>

<script>
/**
 * Display of server's users, and user list related actions.
 */
export default {
	name: "UserList",
	props: {
		users: Array
	} 
};
</script>
