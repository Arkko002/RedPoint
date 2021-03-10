<template>
  <div class="chatbox-container">
    <!-- TODO Iterate over last 20 messages, not all of the messages in server -->
    <li v-for="message in messages" :key="message.id">
			<div class="message">
				<!-- TODO alt="{{message.user.name}}'s avatar"-->
				<img :src="message.user.image" class="message-user-image" />
				<label :value="message.user.username" class="message-user-name"/>
				<input type="button" :value="message.text" class="message-text"/>
			</div>
		</li>

		<div class="message-input-box-div">
			<!-- TODO Rider mixes tabs and spaces on newlining HTML tags, that makes ESLint unhappy -->
			<!-- TODO placeholder="Message {{currentChannel.name}}"-->
			<input type="text" class="message-box-input" id="messageInput" v-model="message"  v-on:keyup.enter="sendMessage(message)"/>
      <button class="message-box-button" :click="sendMessage(message)" />
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
	props: ["messages", "chatUser", "currentChannel"],

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
