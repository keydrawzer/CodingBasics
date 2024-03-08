<template>
	<div class="persons-page">
		<loader-comp :loading="state.loading" />
		<h1>List of Persons</h1>
		<search-box
			@searchByName="searchByNameAndType"
			@searchByType="searchByNameAndType"
		/>
		<person-table :persons="state.persons" />
	</div>
</template>

<script setup>
	import { onMounted, reactive } from "vue";
	import PersonTable from "@/components/PersonTable.vue";
	import SearchBox from "@/components/SearchBox.vue";
	import axios from "/src/utils/axios.js";
	import LoaderComp from "@/components/LoaderComp.vue";

	const state = reactive({
		loading: false,
		persons: [],
	});

	async function fetchData() {
		try {
			state.loading = true;
			const response = await axios.get("/person");
			state.persons = response.data;
			state.persons.sort((a, b) => a.businessEntityID - b.businessEntityID);
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
				`/person/filter?name=${name}&type=${type}`
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
		width: 75%;
		margin-top: 80px;
		margin-bottom: 50px;
		display: flex;
		flex-direction: column;
		align-items: flex-start;
	}
</style>
