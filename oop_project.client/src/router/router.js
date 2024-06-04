import { createRouter, createWebHistory } from 'vue-router'
const HomeView = () => import('../views/ProjectCreationView.vue')
const ProductAPIView = () => import('../views/ProductAPIView.vue')


const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    { path: '/', name: 'home', component: HomeView },
    { path: '/folder/:id(\\d+)', name: 'Products', component: HomeView},
    { path: '/Products', name: 'Products', component: ProductAPIView },
    //{ path: '/about', name: 'about', component: AboutView }
    // Добавьте другие маршруты здесь
  ]
})

export default router