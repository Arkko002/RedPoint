<template>
  <div class="chat-container">
    <ServerList v-bind:serverArray="serverArray"/>
    <UserList v-bind:userArray="userArray"/>
  </div>
</template>

<script>
import ServerList from '@/components/chat/ServerList.vue'
import UserList from '@/components/chat/UserList.vue'

export default {
  name: "Chat",
    components: {
        ServerList,
        UserList
    }

  computed: {
    serverArray() {
      // TODO Call API to get server list
    },

    userArray() {
      // TODO Call API to get user list
    }
  }
};
</script>