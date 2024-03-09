<template>
	<div class="sales-page">
		<loader-comp :loading="state.loading" />
		<h1>Sales</h1>
		<sales-search-box
			@searchByName="searchByNameAndYear"
			@searchByYear="searchByNameAndYear"
		/>
		<sales-table :sales="state.sales" />
	</div>
</template>

<script setup>
	import { onMounted, reactive } from "vue";
	import SalesTable from "@/components/SalesTable.vue";
	import SalesSearchBox from "@/components/SalesSearchBox.vue";
	import axios from "/src/utils/axios.js";
	import LoaderComp from "@/components/LoaderComp.vue";

	const state = reactive({
		loading: false,
		sales: [],
	});

	async function fetchData() {
		try {
			state.loading = true;
			const response = await axios.get("/sales/GetOverviewByPersons");
			state.sales = response.data;
		} catch (error) {
			console.error(error);
		} finally {
			state.loading = false;
		}
	}

	async function searchByNameAndYear(name, year) {
		try {
			state.loading = true;
			var endpoint = "";
			if (year !== null && year !== undefined && year !== '') {
				endpoint = `/sales/GetSalesByPersonAndYear?person=${name}&year=${year}`;
			} else {
				endpoint = `/sales/GetSalesByPersonAndYear?person=${name}`;
			}
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
		width: 75%;
		margin-top: 80px;
		margin-bottom: 50px;
		display: flex;
		flex-direction: column;
		align-items: flex-start;
	}
</style>
