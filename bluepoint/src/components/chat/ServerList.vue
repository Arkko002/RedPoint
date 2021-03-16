<template>
  <div class="server-list-container">
  	<div v-if="servers">
		<li v-for="server in servers" :key="server.id">
			<div class="server-list-button">
				<input class="server-radio-button" type="radio" name="server-radio-button" :value="server.name" />
<!--				alt="{{server.name}}'s image"-->
				<img class="server-image" :src="server.image" />
			</div>
		</li>
	</div>
	
	<!-- TODO Adding servers -->
	<button class="server-radio-button">Add Server</button>
  </div>
</template>

<script>
/**
 * Display of user's server list, and server list actions.
 */
export default {
	name: "ServerList",
	props: {
		servers: Array
	} 
};
</script>
