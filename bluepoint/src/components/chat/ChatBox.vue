<template>
  <div class="chatbox-container">
    <!-- TODO Iterate over last 20 messages, not all of the messages in server -->
    <li v-for="message in messages" :key="message.id">
      <label>
        <img
          v-bind:src="message.user.image"
          class="message message-user-image"
        />
        <label
          v-bind:value="message.user.username"
          class="message message-user-name"
        />
        <input
          type="button"
          v-bind:value="message.text"
          class="message message-text"
        />
      </label>
    </li>

    <div class="message-box">
      <input type="text" class="message-box-input" id="messageInput"  v-model="message"/>
      <button
        class="message-box-button"
        v-on:click="sendMessage(message)"
      />
    </div>
  </div>
</template>

<script>
  import ChatService from "@/common/chat.service"


  export default {
  name: "ChatBox",
  props: ["messages"],


  methods: {
    sendMessage(messageText) {
      if (messageText.length === 0) return;

      const message = {
        text: messageText,
        datetime: Date.now(),
        user: this.currentUser
      };

      ChatService.sendMessage(message)
    }
  }
};
</script>
