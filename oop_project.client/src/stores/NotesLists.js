import { defineStore } from 'pinia';

class NoteList {
  constructor(id, text, Notes = []) {
    this.id = id;
    this.text = text;
    this.Notes = Notes;
  }
}

export const useFolderStore = defineStore('folder', {
  state: () => ({
    noteList: []
  }),
  actions: {
    getFolderById(id) {
      return this.folders.find(folder => folder.id === id);
    },
    getNotesListsById(id) {
      const folder = this.getFolderById(id);
      return folder ? folder.childrenNotesLists : [];
    },
    createNotesList(id, text, position, parent) {
      const newNoteList = new NoteList(id, text, position);
      this.noteList.push(newNoteList);
      return newNoteList;
    }
  }
});
