// stores/user.js
import { defineStore } from 'pinia';

export const useUserStore = defineStore('user', {
  state: () => ({
    username: '',
    email: '',
  }),
  getters: {
    getUserInfo() {
      return `Username: ${this.username}, Email: ${this.email}`;
    },
  },
  actions: {
    setUsername(username) {
      this.username = username;
    },
    setEmail(email) {
      this.email = email;
    },
  },
});
