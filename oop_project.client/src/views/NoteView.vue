<template>
<h3>NoteName</h3>
</template>

<script>
export default {
  name: "NoteView",
  display: "Nested",
  order: 15,
  data() {
    return {
      list: [
        {
          type: 0,
          name: "task 1",
          tasks: [
            {
              type: 0,
              name: "task 2",
              tasks: [],
            },
          ],
        },
        {
          type: 0,
          name: "task 3",
          tasks: [
            {
              type: 1,
              name: "task 4",
              tasks: [],
            },
          ],
        },
        {
          type: 1,
          name: "task 5",
          tasks: [],
        },
      ],
    };
  },
  methods: {
    updateOrder() {
      this.list.forEach((item, index) => {
        console.log("e");
        item.order = index + 1;
        this.list.length;
      });
    },
  },
};
</script>
<style scoped>
.NavigationTree {
  min-height: 50px;
  outline: 1px dashed;
}
</style>
