<template>
  <div class="server-list">
	<div v-if="servers">
		<li v-for="server in servers" :key="server.id">
			<div class="server-list__button-container">
				<input class="server-list__radio-button" type="radio" :value="server.name" />
<!--				alt="{{server.name}}'s image"-->
				<img class="server-list__image" :src="server.image" />
			</div>
		</li>
	</div>
	
	<!-- TODO Adding servers -->
	<button class="server-list__add-server-button">Add Server</button>
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
