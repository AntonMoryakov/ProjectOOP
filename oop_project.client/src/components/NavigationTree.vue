<script setup>
import { defineStore } from "pinia";
import { ref, watch } from "vue";

const useFolderStore = defineStore('folder', {
  state: () => ({
    folders: [{
        id: 0,
        name: "1",
        position: "0",
        parent: 0,
        childrenFolders: [],
        childrenNotesLists: [],
      }]
  }),
});

const folderStore = useFolderStore();

const folders = ref(folderStore.folders);

watch(folders, (newFolders) => {
  folderStore.folders = newFolders;
}, { deep: true });



</script>
<template>
  <div>
    <div>
      <v-list padding bordered class="rounded-borders">
        <nested-draggable
          :folders="folders"
          :empty="false"
          :onUpdate="updateOrder"
        />
      </v-list>
    </div>

    <rawDisplayer class="col-3" :value="list" title="List" />
  </div>
</template>

<script>
import nestedDraggable from "./NestedDragable.vue";
export default {
  name: "nested-example",
  display: "Nested",
  order: 15,
  components: {
    nestedDraggable,
  },
  data() {
    return {
      list: [
        {
          id: 0,
          type: 0,
          name: "task 1",
          tasks: [
            {
              id: 1,
              type: 0,
              name: "task 2",
              tasks: [],
            },
          ],
        },
        {
          id: 2,
          type: 0,
          name: "task 3",
          tasks: [
            {
              id: 3,
              type: 1,
              name: "task 4",
              tasks: [],
            },
          ],
        },
        {
          id: 4,
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
        item.order = index;
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
