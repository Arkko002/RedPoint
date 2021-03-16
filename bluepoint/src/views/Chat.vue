<template>
	<div class="chat-container" v-if="this.chatUser">
    <TheToolbar :user="chatUser" />
    <ServerList :serverArray="chatUser.servers" />
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
	
	data() {
		return {
			chatUser: null,
			currentServer: 0,
			currentChannel: 0,

		};
	},

	methods: {
		fetchInitData() {
			//TODO Caching
			let chatComponent = this;
			
			ChatService.fetchChatUser()
				.then(response => {
					chatComponent.chatUser = response.data;
					
					ChatService.fetchChannelData(chatComponent.chatUser.currentChannelId)
						.then(response => {
							chatComponent.currentChannel = response.data;
						}).catch(error=> {
							chatComponent.$store.dispatch("alert/error", error);
						});

					ChatService.fetchServerData(chatComponent.chatUser.currentServerId)
						.then(response => {
							chatComponent.currentServer = response;
						}).catch(error => {
							chatComponent.$store.dispatch("alert/error", error);
						});
				}).catch(error => {
					chatComponent.$store.dispatch("alert/error", error);
				});
		}
	},
	
	mounted() {
		//Data fetching has to be wrapped in a component method to preserve component reference in "this" keyword
		this.fetchInitData();
	},
	
	beforeDestroy() {
		//TODO Current server and channel should be updated in back-end model per change
		// ChatService.sendClosingData([this.currentServerId, this.currentChannelId]);
	},

	
};
</script>
