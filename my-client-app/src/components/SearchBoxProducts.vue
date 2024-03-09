<template>
  <div class="search-container">
    <div class="search-group">
      <label for="nameSearch">Search by Name:</label>
      <input
        v-model="state.searchText"
        @change="
          () => $emit('searchByName', state.searchText, state.selectedCategory)
        "
        placeholder="Enter name"
        class="search-input box-shadow"
        id="nameSearch"
      />
    </div>
    <div class="search-group">
      <label for="categorySearch">Search by Product Category:</label>
      <select
        v-model="state.selectedCategory"
        @change="
          () =>
            $emit('searchByCategory', state.searchText, state.selectedCategory)
        "
        class="search-select box-shadow"
        id="categorySearch"
      >
        <option value="">None</option>
        <option value="Bikes">Bikes</option>
        <option value="Components">Components</option>
        <option value="Clothing">Clothing</option>
        <option value="Accesories">Accesories</option>
      </select>
    </div>
  </div>
</template>
<script setup>
import { reactive } from "vue";

const state = reactive({
  searchText: "",
  selectedCategory: "",
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