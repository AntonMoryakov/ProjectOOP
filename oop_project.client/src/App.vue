<script setup>
import { defineAsyncComponent } from 'vue'

import ToolBar from './components/ToolBar.vue';
const NavigationTree = defineAsyncComponent(() => import('./components/NavigationTree.vue'));
</script>


<template>
  <v-layout>
    
    <v-navigation-drawer v-model="drawer" width="350">
      <NavigationTree />
    </v-navigation-drawer>

    <v-app-bar>
      <ToolBar :changeDrawerState="changeDrawerState"/>
    </v-app-bar>

    

    <v-main class="d-flex align-center justify-center">
      <router-view />
    </v-main>

    </v-layout>
</template>

<script>
  export default {
    data: () => ({ drawer: true }),
    methods: {
      changeDrawerState(){
        this.drawer= !this.drawer
      }
    }
  }
</script>