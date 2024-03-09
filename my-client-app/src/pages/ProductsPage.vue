<template>
  <div class="products-page">
    <loader-comp :loading="state.loading" />
    <h1>List of Products</h1>
    <search-box :searchLabelText="'Search by Name:'" :selectLabelText="'Search by Category:'"
      :selectOptions="categoryOptions" @searchByName="searchByNameAndCategory"
      @filterByOption="searchByNameAndCategory" />
    <product-table :products="state.products" />
  </div>
</template>

<script setup>
import { onMounted, reactive } from "vue";
import ProductTable from "@/components/ProductTable.vue";
import SearchBox from "@/components/SearchBox.vue";
import axios from "/src/utils/axios.js";
import LoaderComp from "@/components/LoaderComp.vue";

const categoryOptions = [
  { value: '', label: 'None' },
  { value: 'Bikes', label: 'Bikes' },
  { value: 'Components', label: 'Components' },
  { value: 'Clothing', label: 'Clothing' },
  { value: 'Accessories', label: 'Accessories' }
];


const state = reactive({
  loading: false,
  products: [],
});

async function fetchData () {
  try {
    state.loading = true;
    const response = await axios.get("/api/products");
    state.products = response.data;
    state.products.sort((a, b) => a.productId - b.productId);
  } catch (error) {
    console.error(error);
  } finally {
    state.loading = false;
  }
}

async function searchByNameAndCategory (name, category) {
  let endpoint;

  if (name.trim() && category) endpoint = `/api/products/name/${name}/category/${category}`;
  else if (name.trim()) endpoint = `/api/products/name/${name}`;
  else if (category) endpoint = `/api/products/category/${category}`;
  else endpoint = '/api/products';

  try {
    state.loading = true;
    const response = await axios.get(
      endpoint
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
  width: 80%;
  margin-top: 80px;
  margin-bottom: 50px;
  display: flex;
  flex-direction: column;
  align-items: flex-start;
}
</style>
