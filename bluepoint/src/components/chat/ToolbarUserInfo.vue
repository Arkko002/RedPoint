<template>
  <div class="user-info">
		<!-- TODO alt="{{user.name}}'s avatar"-->
		<img class="user-info__image" :src="user.image" />
		<input class="user_info__button" type="button" :value="user.username" />
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
