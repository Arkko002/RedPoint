<template>
	<div></div>

</template>

<script>
import { HubConnection } from "signalr";

//TODO
export default {
	name: "ChannelList",
	
	data() {
		return {
			selectedChannelUniqueId: String,

			channels: Array
		};
	},

	props: {
		connection: HubConnection
	},

	created() {
		let channelList = this;

		this.connection.on("ChannelChanged", (newChannelUniqueId) => {
			channelList.selectedChannelUniqueId = newChannelUniqueId;
			channelList.channelChanged();
		});

		this.connection.on("ChannelAdded", (channel) => {
			channelList.channels.push(channel);
		});

		this.connection.on("ChannelDeleted", (channelId) => {
			channelList.channels = channelList.channels.filter(channel => channel.Id !== channelId);
		});
	},

	methods: {
		addChannel() {
			//TODO Add locally, confirm with event from signalr
			// this.connection.invoke("AddChannel", channel).catch(); //TODO
		},

		deleteChannel() {
			// this.connection.invoke("DeleteChannel", channel).catch(); //TODO
		},		

		changeChannel() {
			// this.connection.invoke("ChangeChannel", currentChannelUnqiueId, newChannelUniqueId).catch(); //TODO	
		},

		channelChanged() {
			//TODO UI change when channel changed
		}
	}
};
</script>

<style>

</style>
