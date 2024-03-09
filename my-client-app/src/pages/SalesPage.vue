<template>
  <div class="sales-page">
    <loader-comp :loading="state.loading" />
    <h1>List of Sales</h1>
    <search-box :searchLabelText="'Search by Seller:'" :selectLabelText="'Search by Year:'" :selectOptions="yearOptions"
      @searchByName="searchByNameAndYear" @filterByOption="searchByNameAndYear" />
    <sale-table :sales="state.sales" />
  </div>
</template>

<script setup>
import { onMounted, reactive } from "vue";
import SaleTable from "@/components/SaleTable.vue";
import SearchBox from "@/components/SearchBox.vue";
import axios from "/src/utils/axios.js";
import LoaderComp from "@/components/LoaderComp.vue";

const yearOptions = [
  { value: '', label: 'All' },
  { value: '2011', label: '2011' },
  { value: '2012', label: '2012' },
  { value: '2013', label: '2013' },
  { value: '2014', label: '2014' },
];

const state = reactive({
  loading: false,
  sales: [],
});

async function fetchData () {
  try {
    state.loading = true;
    const response = await axios.get("/api/sales");
    state.sales = response.data;
    state.sales.sort((a, b) => a.salesOrderId - b.salesOrderId);
  } catch (error) {
    console.error(error);
  } finally {
    state.loading = false;
  }
}

async function searchByNameAndYear (name, year) {
  let endpoint;

  if (name?.trim() && year) endpoint = `/api/sales/${name}/${year}`;
  else if (name?.trim()) endpoint = `/api/sales/${name}`;
  else if (year) endpoint = `/api/sales/${year}`;
  else endpoint = '/api/sales';

  try {
    state.loading = true;
    const response = await axios.get(
      endpoint
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
.sales-page {
  width: 80%;
  margin-top: 80px;
  margin-bottom: 50px;
  display: flex;
  flex-direction: column;
  align-items: flex-start;
}
</style>
