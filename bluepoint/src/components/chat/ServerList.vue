<template>
  <div class="server-list-container">
    <li v-for="server in serverArray" :key="server.id">
			<div class="server-list-button">
				<input class="server-radio-button" type="radio" name="server-radio-button" :value="server.name" />
				<img class="server-image" :src="server.image" alt="{{server.name}}'s image"/>
			</div>
		</li>
  </div>
</template>

<script>
/**
 * Display of user's server list, and server list actions.
 */
export default {
	name: "ServerList",
	props: ["serverArray"]
};
</script>
