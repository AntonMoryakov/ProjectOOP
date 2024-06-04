
<template>
  <draggable
    class="dragArea"
    :list="folders"
    :group="{ name: 'g1' }"
    item-key="name"
    @end="onUpdate"
    :clickable="false"
  >
    <template #item="{ element }">
      <div>
        <v-list-item
          :prepend-icon="selectIcon(element)"
          :title="element.name"
          :key="element.id"
          :to="computeRoute(element.id, element.type)"
          :append-icon="element.type == 0 ? element.expanded ? 'mdi-chevron-down' : 'mdi-chevron-down' : ''"
        >
        </v-list-item>
        <nested-draggable
          v-if="element.type == 0"
          class="ml-4"
          :folders="element.tasks"
          :onUpdate="onUpdate"
          :empty="element.tasks.length == 0"
        />
        
      </div>
    </template>
    <template #header v-if="empty">
      <v-list-item
          prepend-icon="mdi-information-outline"
          title="Пока что это пустая папка"
          :key="0"
        >
        </v-list-item> </template>
    <template #footer> </template>
  </draggable>
</template>
<script>
import draggable from "vuedraggable";
export default {
  props: {
    folders: {
      required: true,
      type: Array,
    },
    empty: {
      required: false,
      type: Boolean,
    },
    onUpdate: {
      required: false,
      type: Function,
    },
  },
  components: {
    draggable,
  },
  name: "nested-draggable",
  methods: {
    selectIcon(el) {
      switch (el.type) {
        case 0:
          return "mdi-folder";
        case 1:
          return "mdi-note";
        default:
          return "mdi-information-outline";
      }
    },
    computeRoute(id, type){
      return '/' + (type == 0 ? 'folder' : 'note') + '/' + id
    }
  },
  computed: {
  },
};
</script>
<style scoped>
.dragArea {
  min-height: 50px;
}
.folder-row {
  display: flex;
  align-items: center;
  justify-content: space-between;
}
.icon,
.toggle-icon {
  width: 24px;
  height: 24px;
}
.folder-name {
  flex-grow: 1;
  margin: 0 10px;
}
.nested-files {
  margin-left: 34px; /* Отступ для вложенных файлов */
}
</style>
<!-- <v-expansion-panel>
          <v-expansion-panel :title="element.name" />
          <v-expansion-panel-text>
            <nested-draggable :tasks="element.tasks" :onUpdate="onUpdate" />
          </v-expansion-panel-text>
        </v-expansion-panel> -->
