<template>
	<div class="search-container">
		<div class="search-group">
			<label for="textSearch">{{ props.searchLabelText }}</label>
			<input v-model="state.searchText" @change="() => $emit('searchByName', state.searchText, state.selectedOption)
				" :placeholder="props.placeholderText" class="search-input box-shadow" id="textSearch" />
		</div>
		<div class="search-group">
			<label for="optionsFilter">{{ props.selectLabelText }}</label>
			<select v-model="state.selectedOption" @change="() => $emit('filterByOption', state.searchText, state.selectedOption)
				" class="search-select box-shadow" id="optionsFilter">
				<option v-for="option in props.selectOptions" :value="option.value" :key="option.value">{{ option.label }}
				</option>
			</select>
		</div>
	</div>
</template>
<script setup>
import { defineProps, reactive } from 'vue';

const props = defineProps({
	searchLabelText: String,
	selectLabelText: String,
	placeholderText: {
		type: String,
		default: 'Enter name'
	},
	selectOptions: Array
});

const state = reactive({
	searchText: "",
	selectedOption: "",
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
