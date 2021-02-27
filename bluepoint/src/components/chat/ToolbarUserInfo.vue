<template>
  <div class="user-info-container">
		<img :src="user.image" alt="{{user.name}}'s avatar"/>
		<input type="button" :value="user.name" />
  </div>
</template>

<script>
/**
 * Current user's brief account info, and quick access to account actions.
 */
export default {
	name: "ToolbarUserInfo",
	props: ["user"]
};
</script>
