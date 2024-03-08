import { createRouter, createWebHistory } from 'vue-router';
import Home from '/src/pages/HomePage.vue';
import Persons from '/src/pages/PersonsPage.vue';
import Products from '/src/pages/ProductsPage.vue';
import SalesPerson from '/src/pages/SalesPersonPage.vue';
import SalesOverview from '/src/pages/SalesOverviewPage.vue';

const routes = [
  { path: '/', component: Home },
  { path: '/persons', component: Persons },
  { path: '/products', component: Products },
  { path: '/sales-person', component: SalesPerson },
  { path: '/sales-overview', component: SalesOverview },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;