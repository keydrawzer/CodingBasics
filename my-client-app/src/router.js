import { createRouter, createWebHistory } from 'vue-router';
import Home from '/src/pages/HomePage.vue';
import Persons from '/src/pages/PersonsPage.vue';
import Products from '/src/pages/ProductsPage.vue';
import Sales from '/src/pages/SalesPage.vue';

const routes = [
  { path: '/', component: Home },
  { path: '/persons', component: Persons },
  { path: '/products', component: Products },
  { path: '/sales', component: Sales },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;