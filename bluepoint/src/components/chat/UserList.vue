<template>
  <div class="user-container">
    <li v-for="user in userArray" :key="user.id">
			<button class="user-list-button">
				<img class="user-list-avatar" :src="user.image" alt="{{user.name}}'s avatar"/>
				<label class="user-list-name" :content="user.name" />
			</button>
    </li>
  </div>
</template>

<script>
/**
 * Display of server's users, and user list related actions.
 */
export default {
	name: "UserList",
	props: ["userArray"]
};
</script>
