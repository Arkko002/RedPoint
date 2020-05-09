<template>
  <div class="user-info-container">
    <label>
      <input type="button" v-bind:value="user.name" />
      <img v-bind:src="user.image" />
    </label>
  </div>
</template>

<script>
export default {
  name: "ToolbarUserInfo",
  props: ["user"]
};
</script>
