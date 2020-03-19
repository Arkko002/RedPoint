<template>
  <div class="chatbox-container">
    <!-- TODO Iterate over last 20 messages, not all of the messages in server -->
    <li v-for="message in messageArray" :key="message.id">
      <label>
        <img
          v-bind:src="message.user.imagepath"
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
      <input type="text" class="message-box-input" id="messageInput" />
      <button
        class="message-box-button"
        v-on:click="sendMessage(messageInput.value)"
      />
    </div>
  </div>
</template>

<script>
export default {
  name: "ChatBox",
  props: ["messageArray"],

  methods: {
    sendMessage(message) {
      if (message.length == 0) return;

      this.$emit("message-sent", message);
    }
  }
};
</script>
