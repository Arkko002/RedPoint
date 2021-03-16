<template>
  <div class="user-info-container">
		<!-- TODO alt="{{user.name}}'s avatar"-->
		<img :src="user.image" />
		<input type="button" :value="user.username" />
  </div>
</template>

<script>
/**
 * Current user's brief account info, and quick access to account actions.
 */
export default {
	name: "ToolbarUserInfo",
	props: {
		user: Object
	},
};
</script>
