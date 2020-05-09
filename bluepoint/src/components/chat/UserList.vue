<template>
  <div class="user-container">
    <li v-for="user in userArray" :key="user.id">
      <label>
        <input type="button" v-bind:value="user.name" />
        <img v-bind:src="user.image" />
      </label>
    </li>
  </div>
</template>

<script>
export default {
  name: "UserList",
  props: ["userArray"]
};
</script>
