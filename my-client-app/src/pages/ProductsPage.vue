<template>
    <div class="products-page">
      <loader-comp :loading="state.loading" />
      <h1>List of Products</h1>
      <search-box
        @searchByName="searchByNameAndType"
        @searchByType="searchByNameAndType"
      />
      <product-table :products="state.products" />
    </div>
</template>
  
  <script setup>
  import { onMounted, reactive } from 'vue';
  import ProductTable from '@/components/ProductTable.vue';
  import SearchBox from '@/components/SearchProductsBox.vue';
  import axios from '/src/utils/axios.js';
  import LoaderComp from '@/components/LoaderComp.vue';
  
  const state = reactive({
    loading: false,
    products: [],
  });
  
  async function fetchData() {
    try {
      state.loading = true;
      const response = await axios.get('/product');
      state.products = response.data;
      state.products.sort((a, b) => a.productId - b.productId);
    } catch (error) {
      console.error(error);
    } finally {
      state.loading = false;
    }
  }
  
  async function searchByNameAndType(name, type) {
    try {
      state.loading = true;
      const response = await axios.get(
        `/product/GetByNameAndCategoryType?name=${name}&categoryType=${type}`
      );
      state.products = response.data;
    } catch (error) {
      console.error(error);
    } finally {
      state.loading = false;
    }
  }
  
  onMounted(async () => {
    await fetchData();
  });
  </script>
  
  <style scoped>
  .products-page {
    width: 75%;
    margin-top: 80px;
    margin-bottom: 50px;
    display: flex;
    flex-direction: column;
    align-items: flex-start;
  }
  </style>
  
