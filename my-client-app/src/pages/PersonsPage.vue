<template>
	<div class="persons-page">
		<loader-comp :loading="state.loading" />
		<h1>List of Persons</h1>
		<search-box :searchLabelText="'Search by Name:'" :selectLabelText="'Search by Type:'" :selectOptions="typeOptions"
			@searchByName="searchByNameAndType" @filterByOption="searchByNameAndType" />
		<person-table :persons="state.persons" />
	</div>
</template>

<script setup>
import { onMounted, reactive } from "vue";
import PersonTable from "@/components/PersonTable.vue";
import SearchBox from "@/components/SearchBox.vue";
import axios from "/src/utils/axios.js";
import LoaderComp from "@/components/LoaderComp.vue";

const typeOptions = [
	{ value: '', label: 'None' },
	{ value: 'IN', label: 'IN' },
	{ value: 'EM', label: 'EM' },
	{ value: 'SP', label: 'SP' },
	{ value: 'SC', label: 'SC' },
	{ value: 'VC', label: 'VC' },
	{ value: 'GC', label: 'GC' }
];

const state = reactive({
	loading: false,
	persons: [],
});

async function fetchData () {
	try {
		state.loading = true;
		const response = await axios.get("/api/persons");
		state.persons = response.data;
		state.persons.sort((a, b) => a.businessEntityID - b.businessEntityID);
	} catch (error) {
		console.error(error);
	} finally {
		state.loading = false;
	}
}

async function searchByNameAndType (name, type) {
	let url;

	if (name.trim() && type) url = `/api/persons/name/${name}/type/${type}`;
	else if (name.trim()) url = `/api/persons/name/${name}`;
	else if (type) url = `/api/persons/type/${type}`;
	else url = '/api/persons';

	try {
		state.loading = true;
		const response = await axios.get(
			url
		);
		state.persons = response.data;
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
.persons-page {
	width: 80%;
	margin-top: 80px;
	margin-bottom: 50px;
	display: flex;
	flex-direction: column;
	align-items: flex-start;
}
</style>
