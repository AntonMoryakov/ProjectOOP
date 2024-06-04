import { defineStore } from 'pinia';

class Folder {
  constructor(id, text, position, parent, childrenFolders = [], childrenNotesLists = []) {
    this.id = id;
    this.name = text;
    this.position = position;
    this.parent = parent;
    this.childrenFolders = childrenFolders;
    this.childrenNotesLists = childrenNotesLists;
  }
}

export const useFolderStore = defineStore('folderStore', {
  state: () => ({
    folders: [
      {
        id: 0,
        name: "1",
        position: "0",
        parent: 0,
        childrenFolders: [],
        childrenNotesLists: [],
      }
    ]
  }),
  actions: {
    getFolderById(id) {
      return this.folders.find(folder => folder.id === id);
    },
    getSubfoldersById(id) {
      const folder = this.getFolderById(id);
      return folder ? folder.childrenFolders : [];
    },
    getNotesListsById(id) {
      const folder = this.getFolderById(id);
      return folder ? folder.childrenNotesLists : [];
    },
    updateFolderPosition(id, newPosition) {
      const folder = this.getFolderById(id);
      if (folder) {
        folder.position = newPosition;
      }
    },
    createFolder(id, text, position, parent) {
      const newFolder = new Folder(id, text, position, parent);
      this.folders.push(newFolder);
      return newFolder;
    }
  }
});
