<template>
  <div class="chatbox">
    <!-- TODO Iterate over last 20 messages, not all of the messages in server -->
	<div v-if="messages">
		<li v-for="message in messages" :key="message.id" >
			<div class="chatbox__message-container">
				<!-- TODO alt="{{message.user.name}}'s avatar"-->
				<img :src="message.user.image" class="chatbox__user-image" />
				<label :value="message.user.username" class="chatbox__user-name"/>
				<input type="button" :value="message.text" class="chatbox__mesage-text"/>
			</div>
		</li>
	</div>

	<div class="chatbox__input-container">
		<!-- TODO Rider mixes tabs and spaces on newlining HTML tags, that makes ESLint unhappy -->
		<!-- TODO placeholder="Message {{currentChannel.name}}"-->
		<input type="text" class="chatbox__input-field" v-model="message"  v-on:keyup.enter="sendMessage(message)"/>
		<button class="chatbox__input-submit" :click="sendMessage(message)" />
    </div>
  </div>
</template>

<script>
import ChatService from "@/services/chat.service";

/**
 * Displaying and sending messages.
 */
export default {
	name: "ChatBox",
	props: {
		messages: Array,
		chatUser: Object,
		currentChannel: String
	}, 

	data() {
		return {
			message: ""
		};
	},
	
	methods: {
		/**
     * Constructs a message from user input and passes it to the [ChatService]{@link ChatService}
     * @param messageText
     */
		sendMessage(messageText) {
			if (messageText.length === 0) return;

			const message = {
				text: messageText,
				datetime: Date.now(),
				channel: this.currentChannel,
				user: this.chatUser,
			};

			ChatService.sendMessage(message);
		},
	},
};
</script>
