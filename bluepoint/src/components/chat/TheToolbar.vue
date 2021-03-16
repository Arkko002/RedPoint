<template>
  <div class="toolbar-container">
    <ToolbarUserInfo :user="chatUser"/>
    <ToolbarSearchField />
  </div>
</template>

<script>
import ToolbarSearchField from "./ToolbarSearchField.vue";
import ToolbarUserInfo from "./ToolbarUserInfo.vue";

/**
 * Main toolbar component that serves as a container for toolbar tools.
 */
export default {
	name: "TheToolbar",
	props: {
		chatUser: Object
	},

	components: {
		ToolbarSearchField,
		ToolbarUserInfo
	}
};
</script>
