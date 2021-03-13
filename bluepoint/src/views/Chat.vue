<template>
	<div class="chat-container">
    <TheToolbar :user="chatUser" />
    <ServerList :serverArray="serverArray" />
    <UserList :userArray="currentServer.users" />
    <ChatBox :messageArray="currentChannel.messages" :chat-user="chatUser"/>
		<router-view></router-view>
  </div>
</template>

<script>
import ServerList from "@/components/chat/ServerList.vue";
import UserList from "@/components/chat/UserList.vue";
import TheToolbar from "@/components/chat/TheToolbar.vue";
import ChatBox from "@/components/chat/ChatBox.vue";
import ChatService from "../services/chat.service";

/**
 * Main chat view, used as a router path for chat page.
 */
export default {
	name: "Chat",
	components: {
		ServerList,
		UserList,
		TheToolbar,
		ChatBox,
	},

	/**
   * Initializes chat functionality with data pulled from back end.
   */
	created() {
		//TODO Caching
		ChatService.fetchChatUser().then(
			(user) => {
				this.chatUser = user;
			},
			(error) => {
				this.$store.dispatch("alert/error", error);
			});
		
		ChatService.fetchChannelData(this.chatUser.currentChannelId).then(
			(channel) => {
				this.currentChannel = channel;
			},
			(error)=> {
				this.$store.dispatch("alert/error", error);
			});
		
		
		ChatService.fetchServerData(this.chatUser.currentServerId).then(
			(server) => {
				this.currentServer = server;
			},
			(error) => {
				this.$store.dispatch("alert/error", error);
			}
		);
	},

	beforeDestroy() {
		//TODO Current server and channel should be updated in back-end model per change
		// ChatService.sendClosingData([this.currentServerId, this.currentChannelId]);
	},

	data() {
		return {
			chatUser: null,
			currentServer: null,
			currentChannel: null,
			
		};
	},
};
</script>
