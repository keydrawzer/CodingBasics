import { createRouter, createWebHistory } from 'vue-router';
import Home from '/src/pages/HomePage.vue';
import Persons from '/src/pages/PersonsPage.vue';
import Products from '/src/pages/ProductsPage.vue';

const routes = [
  { path: '/', component: Home },
  { path: '/persons', component: Persons },
  { path: '/products', component: Products },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;