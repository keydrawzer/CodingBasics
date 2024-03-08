<template>
	<div class="search-container">
		<div class="search-group">
			<label for="nameSearch">Search by Name:</label>
			<input v-model="state.searchText" @change="() => $emit('searchByName', state.searchText, state.selectedOption)
				" placeholder="Enter name" class="search-input box-shadow" id="nameSearch" />
		</div>
		<div class="search-group">
			<label for="typeSearch">{{ props.dropdownLabel }}</label>
			<select v-model="state.selectedOption" @change="() => $emit('searchBySelection', state.searchText, state.selectedOption)
				" class="search-select box-shadow" id="typeSearch">
				<option value="">None</option>
				<option v-for="(dropdownOption, index) in props.dropdownOptions" :key="index" :value="dropdownOption">
					{{ dropdownOption }}
				</option>
			</select>
		</div>
	</div>
</template>

<script setup>
import { reactive, defineProps } from "vue";

const state = reactive({
	searchText: "",
	selectedOption: "",
});

const props = defineProps({
	dropdownLabel: String,
	dropdownOptions: Array
})
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
