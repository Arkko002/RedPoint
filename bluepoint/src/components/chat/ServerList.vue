<template>
  <div class="server-container">
    <li v-for="server in serverArray" :key="server.id">
      <label>
        <input type="radio" v-bind:value="server.name" />
        <img v-bind:src="server.image" />
      </label>
    </li>
  </div>
</template>

<script>
/**
 * Display of user's server list, and server list actions.
 */
export default {
	name: "ServerList",
	props: ["serverArray"]
};
</script>
