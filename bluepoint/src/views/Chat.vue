<template>
  <div class="chat-container">
    <TheToolbar :user="chatUser" />
    <ServerList :serverArray="serverArray" />
    <UserList :userArray="currentServer.users" />
    <ChatBox :messageArray="currentChannel.messages" :chat-user="chatUser"/>
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
		this.serverArray = ChatService.fetchServers();
		
		this.chatUser = ChatService.fetchChatUser();
		
		this.currentChannel = ChatService.fetchChannelData(this.chatUser.currentChannelId);
		this.currentServer = ChatService.fetchServerData(this.chatUser.currentServerId);
	},

	beforeDestroy() {
		ChatService.sendClosingData([this.currentServerId, this.currentChannelId]);
	},

	data() {
		return {
			serverArray: null,

			chatUser: null,
			currentServer: null,
			currentChannel: null,
			
		};
	},
};
</script>
