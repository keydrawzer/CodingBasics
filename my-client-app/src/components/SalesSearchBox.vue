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
			<label for="typeSearch">Search by Year:</label>
			<select
				v-model="state.selectedType"
				@change="
					() => $emit('searchByYear', state.searchText, state.selectedType)
				"
				class="search-select box-shadow"
				id="typeSearch"
			>
				<option value="">None</option>
				<option value="1950">1950</option>
				<option value="1970">1970</option>
				<option value="1980">1980</option>
				<option value="1990">1990</option>
				<option value="2000">2000</option>
				<option value="2001">2001</option>
				<option value="2002">2002</option>
				<option value="2003">2003</option>
				<option value="2004">2004</option>
				<option value="2005">2005</option>
				<option value="2006">2006</option>
				<option value="2007">2007</option>
				<option value="2008">2008</option>
				<option value="2009">2009</option>
				<option value="2010">2010</option>
				<option value="2011">2011</option>
				<option value="2012">2012</option>
				<option value="2013">2013</option>
				<option value="2014">2014</option>
				<option value="2015">2015</option>
				<option value="2016">2016</option>
				<option value="2017">2017</option>
				<option value="2018">2018</option>
				<option value="2019">2019</option>
				<option value="2020">2020</option>
				<option value="2021">2021</option>
				<option value="2022">2022</option>
				<option value="2023">2023</option>
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
