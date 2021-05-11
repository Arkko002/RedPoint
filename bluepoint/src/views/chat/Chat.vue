<template>
    <div class="chat" v-if="!isFetchingData">
    <TheToolbar :chatUser="chatUser" />
    <ServerList :servers="chatUser.servers" />
    <UserList :users="currentServer.users" />
    <ChatBox :messages="currentChannel.messages" :chat-user="chatUser"/>
    </div>
    <div class="chat--loading" v-else>
        <!-- TODO Loading animation when data is being fetched-->
        Data is loading
    </div>
</template>

<script>
import ServerList from "@/components/chat/ServerList.vue";
import UserList from "@/components/chat/UserList.vue";
import TheToolbar from "@/components/chat/TheToolbar.vue";
import ChatBox from "@/components/chat/ChatBox.vue";
import ChatService from "@/services/chat.service";

import { HubConnection, HubConnectionBuilder } from "signalr";

/**
 * Main chat view, used as a router path for chat page.
 */
export default {
	//TODO Store current channel, server in Chat.vue, pass props
	name: "Chat",
	components: {
		ServerList,
		UserList,
		TheToolbar,
		ChatBox,
	},
	
	data() {
		return {
			isFetchingData: true,

			chatUser: null,
			currentServer: 0,
			currentChannel: 0,

			builder: HubConnectionBuilder,
			connection: HubConnection
		};
	},

	methods: {
		fetchInitData() {
			//TODO Caching
			//TODO Reduce nesting, rework chat.service
			let chat = this;
			chat.isFetchingData = true;

			ChatService.fetchChatUser()
				.then(response => {
					chat.chatUser = response.data;
					chat.fetchCurrentServer();
					chat.fetchCurrentChannel();
				}).catch(error => {
					chat.$store.dispatch("alert/error", error);
				}).finally(() => {
					chat.isFetchingData = false;
				}) ;
		},

		fetchCurrentChannel() {
			ChatService.fetchChannelData(this.chatUser.currentChannelId)
				.then(response => {
					this.currentChannel = response.data;
				}).catch(error=> {
					this.$store.dispatch("alert/error", error);
				});
		},

		fetchCurrentServer() {
			ChatService.fetchServerData(this.chatUser.currentServerId)
				.then(response => {
					this.currentServer = response;
				}).catch(error => {
					this.$store.dispatch("alert/error", error);
				});
		},
	},

	beforeMount() {
		this.builder = new HubConnectionBuilder().withAutomaticReconnect().withUrl("/chatHub");
			
		this.connection = this.builder.build();
	},

	mounted() {
		this.fetchInitData();
		this.connection.StartAsync();
	},
	
	beforeDestroy() {
		//TODO Current server and channel should be updated in back-end model per change
		// ChatService.sendClosingData([this.currentServerId, this.currentChannelId]);

		this.connection.StopAsync();
	},

	
};
</script>
