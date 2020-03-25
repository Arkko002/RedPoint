<template>
  <div class="chat-container">
    <TheToolbar v-bind:user="currentUser" />
    <ServerList v-bind:serverArray="serverArray" />
    <UserList v-bind:userArray="userArray" />
    <ChatBox
      v-bind:messageArray="messageArray"
      v-on:message-sent="sendMessage"
    />
  </div>
</template>

<script>
import ServerList from "@/components/chat/ServerList.vue";
import UserList from "@/components/chat/UserList.vue";
import TheToolbar from "@/components/chat/TheToolbar.vue";
import ChatBox from "@/components/chat/ChatBox.vue";

export default {
  name: "Chat",
  components: {
    ServerList,
    UserList,
    TheToolbar,
    ChatBox
  },

  computed: {
    currentUser() {
      localStorage.getItem("user");
    },

    serverArray() {
      // TODO Call API to get server list
    },

    userArray() {
      // TODO Call API to get user list
    },

    messageArray() {
      // TODO
    }
  },

  methods: {
    sendMessage(message) {
      message = {
        user = this.currentUser,
        messageText = message,
        datetime = Date.now()
      }

      var dto = JSON.stringify(message)
      // TODO send to API
    }
  }
};
</script>
