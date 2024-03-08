<template>
    <div class="sales-persons-page">
        <loader-comp :loading="state.loading" />
        <h1>List of Sales Persons</h1>
        <search-box :dropdownOptions="[2011, 2012, 2013, 2014]" dropdownLabel="Search by Year:"
            @searchByName="searchByNameAndYear" @searchBySelection="searchByNameAndYear" />
        <sales-person-table :persons="state.salesPersons" />
    </div>
</template>

<script setup>
import { onMounted, reactive } from "vue";
import SalesPersonTable from "@/components/SalesPersonTable.vue";
import SearchBox from "@/components/SearchBox.vue";
import axios from "/src/utils/axios.js";
import LoaderComp from "@/components/LoaderComp.vue";

const state = reactive({
    loading: false,
    salesPersons: [],
});

async function fetchData() {
    try {
        state.loading = true;
        const response = await axios.get('/sales-person');
        state.salesPersons = response.data;
        state.salesPersons.sort((a, b) => a.businessEntityID - b.businessEntityID);
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
            `/sales-person/GetByNameAndYear?name=${name}&year=${year}`
        );
        state.salesPersons = response.data;
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
.sales-persons-page {
    width: 75%;
    margin-top: 80px;
    margin-bottom: 50px;
    display: flex;
    flex-direction: column;
    align-items: flex-start;
}
</style>
