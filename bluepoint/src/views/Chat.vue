<template>
  <div class="chat-container">
    <TheToolbar v-bind:user="currentUser" />
    <ServerList v-bind:serverArray="serverArray" />
    <UserList v-bind:userArray="currentServer.users" />
    <ChatBox v-bind:messageArray="currentChannel.messages" />
  </div>
</template>

<script>
import ServerList from "@/components/chat/ServerList.vue";
import UserList from "@/components/chat/UserList.vue";
import TheToolbar from "@/components/chat/TheToolbar.vue";
import ChatBox from "@/components/chat/ChatBox.vue";
import ChatService from "../services/chat.service";

/**
 * Component used as a router view for chat page.
 */
export default {
	name: "Chat",
	components: {
		ServerList,
		UserList,
		TheToolbar,
		ChatBox: ChatBox,
	},

	created() {
		//TODO Caching
		this.serverArray = JSON.parse(ChatService.fetchServers());
		this.currentChannel = JSON.parse(
			ChatService.fetchChannelData(this.currentUser.currentChannelId)
		);
		this.currentServer = JSON.parse(
			ChatService.fetchServerData(this.currentUser.currentServerId)
		);
	},

	beforeDestroy() {
		ChatService.sendClosingData([this.currentServerId, this.currentChannelId]);
	},

	data() {
		return {
			serverArray: null,

			currentServer: null,
			currentChannel: null,
		};
	},
  
	computed: {
		currentUser() {
			return localStorage.getItem("user");
		},
	},
};
</script>
