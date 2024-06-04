<template>
      <v-app-bar-nav-icon @click="changeDrawerState"></v-app-bar-nav-icon>

        <v-toolbar-title>
          Title
        </v-toolbar-title>
</template>

<script>

export default {
  props: {
    changeDrawerState: Function,
  },
};
</script>