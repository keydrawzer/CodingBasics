<template>
    <div class="sales-page">
      <loader-comp :loading="state.loading" />
      <h1>List of Sales</h1>
      <!-- <search-box
        @searchByName="searchByNameAndType"
        @searchByType="searchByNameAndType"
      /> -->
      <sales-table :sales="state.sales" />
    </div>
  </template>
  
  <script setup>
  import { onMounted, reactive } from 'vue';
  import SalesTable from '@/components/SalesTable.vue';
//   import SearchBox from '@/components/SearchBox.vue';
  import axios from '/src/utils/axios.js';
  import LoaderComp from '@/components/LoaderComp.vue';
  
  const state = reactive({
    loading: false,
    sales: [],
  });
  
  async function fetchData() {
    try {
      state.loading = true;
      const response = await axios.get('/sales');
      state.sales = response.data;
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
  .sales-page {
    width: 75%;
    margin-top: 80px;
    margin-bottom: 50px;
    display: flex;
    flex-direction: column;
    align-items: flex-start;
  }
  </style>
  