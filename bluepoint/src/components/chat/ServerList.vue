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
import { HubConnection } from "signalr";

/**
 * Display of user's server list, and server list actions.
 */
export default {
	name: "ServerList",
	props: {
		servers: Array,
		currentServerUniqueId: String,

		connection: HubConnection
	},

	created() {
		let serverList = this;

		this.connection.on("ServerAdded", (server) => {
			serverList.servers.push(server);
		});

		this.connection.on("ServerDeleted", (serverId) => {
			serverList.servers = serverList.servers.filter(server => server.Id !== serverId);
		});

		this.connection.on("ServerChanged", (newServerUniqueId) => {
			serverList.currentServerUniqueId = newServerUniqueId; //TODO
		});
	},

	methods: {
		addServer(server) {
			this.connection.invoke("AddServer", server).catch(); //TODO
		},

		deleteServer(serverId) {
			this.connection.invoke("DeleteServer", serverId).catch(); //TODO 
		},

		changeServer(newServerUniqueId) {
			//TODO
			this.connection.invoke("ChangeServer", this.currentServerUniqueId, newServerUniqueId);
		}
	}


};
</script>
