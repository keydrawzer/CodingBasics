<template>
	<div class="overview-page">
		<loader-comp :loading="state.loading" />
		<h1>Sales Overview</h1>
		<SalesOverviewTable :overviews="state.overviews" />
	</div>
</template>

<script setup>
import { onMounted, reactive } from "vue";
import axios from "/src/utils/axios.js";
import LoaderComp from "@/components/LoaderComp.vue";
import SalesOverviewTable from "@/components/SalesOverviewTable.vue";

const state = reactive({
	loading: false,
	overviews: [],
});

async function fetchData () {
	try {
		state.loading = true;
		const response = await axios.get("/api/sales/overview");
		state.overviews = response.data;
		state.overviews.sort((a, b) => a.sellerID - b.sellerID);
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
.overview-page {
	width: 95%;
	margin-top: 60px;
	margin-bottom: 50px;
	display: flex;
	flex-direction: column;
	align-items: flex-start;
}
</style>
