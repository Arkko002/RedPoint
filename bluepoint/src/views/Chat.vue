<template>
  <div class="chat-container">
    <TheToolbar v-bind:user="currentUser" />
    <ServerList v-bind:serverArray="serverArray" />
    <UserList v-bind:userArray="currentServer.users" />
    <ChatBox v-bind:messageArray="currentChannel.messages"/>
  </div>
</template>

<script>
import ServerList from "@/components/chat/ServerList.vue";
import UserList from "@/components/chat/UserList.vue";
import TheToolbar from "@/components/chat/TheToolbar.vue";
import ChatBox from "@/components/chat/ChatBox.vue";
import ChatService from "bluepoint/src/common/chat.service"

export default {
  name: "Chat",
  components: {
    ServerList,
    UserList,
    TheToolbar,
    ChatBox: ChatBox
  },
  
  created() {
    //TODO Caching
    this.serverArray = ChatService.fetchServers();
    this.currentChannel = ChatService.fetchChannelData(this.currentUser.currentChannelId)
    this.currentServer = ChatService.fetchServerData(this.currentUser.currentServerId)
  },
  
  beforeDestroy() {
    ChatService.sendClosingData([this.currentServerId, this.currentChannelId])
  },
  
  data() {
    return {
      serverArray: null,
      
      /**
       * @param currentServer.users   List of user on the server
       */
      currentServer: null,

      /**
       * @param currentChannel.messages   List of messages in the channel
       */
      currentChannel: null,
    }
  },
  
  computed: {
    currentUser() {
      localStorage.getItem("user");
    },
  },
};
</script>
