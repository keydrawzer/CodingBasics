<template>
  <div class="product-page">
    <loader-comp :loading="state.loading" />
    <h1>List of Sales</h1>
    <search-box-sales
      @searchByName="searchByNameAndYear"
      @searchByYear="searchByNameAndYear"
    />
    <sales-table :products="state.sales" />
  </div>
</template>

<script setup>
import { onMounted, reactive } from "vue";
import SalesTable from "@/components/SalesTable.vue";
import SearchBoxSales from '@/components/SearchBoxSales.vue'
import axios from "/src/utils/axios.js";
import LoaderComp from "@/components/LoaderComp.vue";

const state = reactive({
  loading: false,
  sales: [],
});

async function fetchData() {
  try {
    state.loading = true;
    const response = await axios.get("/sales");
    state.sales = response.data;
    state.sales.sort((a, b) => a.salesPersonID - b.salesPersonID);
  } catch (error) {
    console.error(error);
  } finally {
    state.loading = false;
  }
}

async function searchByNameAndYear(name, year) {
  try {
    state.loading = true;
    const response = await axios.get(
      `/sales/GetByNameAndYear?name=${name}&year=${year}`
    );
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
.product-page {
  width: 75%;
  margin-top: 80px;
  margin-bottom: 50px;
  display: flex;
  flex-direction: column;
  align-items: flex-start;
}
</style>