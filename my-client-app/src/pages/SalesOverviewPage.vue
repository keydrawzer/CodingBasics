<template>
    <div class="sales-overview-page">
        <loader-comp :loading="state.loading" />
        <h1>Sales Overview</h1>
        <sales-overview-table :salesData="state.salesData" />
    </div>
</template>

<script setup>
import { onMounted, reactive } from "vue";
import SalesOverviewTable from "@/components/SalesOverviewTable.vue";
import axios from "/src/utils/axios.js";
import LoaderComp from "@/components/LoaderComp.vue";

const state = reactive({
    loading: false,
    salesData: [],
});

async function fetchData() {
    try {
        state.loading = true;
        const response = await axios.get('/sales-overview');
        state.salesData = response.data;
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
.sales-overview-page {
    width: 75%;
    margin-top: 80px;
    margin-bottom: 50px;
    display: flex;
    flex-direction: column;
    align-items: flex-start;
}
</style>
