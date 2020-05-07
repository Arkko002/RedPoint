<template>
  <div class="toolbar-container">
    <ToolbarUserInfo />
    <ToolbarSearchField />
  </div>
</template>

<script>
import ToolbarSearchField from "./ToolbarSearchField.vue";
import ToolbarUserInfo from "./ToolbarUserInfo.vue";

export default {
  name: "TheToolbar",
  components: {
    ToolbarSearchField,
    ToolbarUserInfo
  }
};
</script>
