<template>
	<div class="search-container">
		<div class="search-group">
			<label for="nameSearch">Search by Name:</label>
			<input
				v-model="state.searchText"
				@change="
					() => $emit('searchByName', state.searchText, state.selectedType)
				"
				placeholder="Enter name"
				class="search-input box-shadow"
				id="nameSearch"
			/>
		</div>
		<div class="search-group">
			<label for="typeSearch">Search by Person Type:</label>
			<select
				v-model="state.selectedType"
				@change="
					() => $emit('searchByType', state.searchText, state.selectedType)
				"
				class="search-select box-shadow"
				id="typeSearch"
			>
				<option value="">None</option>
				<option value="IN">IN</option>
				<option value="EM">EM</option>
				<option value="SP">SP</option>
				<option value="SC">SC</option>
				<option value="VC">VC</option>
				<option value="GC">GC</option>
			</select>
		</div>
	</div>
</template>
<script setup>
	import { reactive } from "vue";

	const state = reactive({
		searchText: "",
		selectedType: "",
	});
</script>
<style>
	.search-container {
		margin-bottom: 20px;
		display: flex;
		justify-content: center;
	}

	.search-container label {
		margin-right: 15px;
	}

	.search-input,
	.search-select {
		font-size: 16px;
		font-family: "Roboto", sans-serif;
		padding: 10px;
		margin-right: 10px;
		border-radius: 4px;
		border: 1px solid #ddd;
	}

	.search-input {
		width: 400px;
	}
</style>
