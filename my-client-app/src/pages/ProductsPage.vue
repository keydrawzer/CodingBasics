<template>
    <div class="product-page">
        <loader-comp :loading="state.loading" />
        <h1>List of Product</h1>
        <search-box
            @searchByName="searchByName"
            @searchByCategory="searchByCategory"
        />
        <product-table :products="state.products" />
    </div>
</template>

<script setup>
    import { onMounted, reactive } from "vue";
    import ProductTable from "@/components/ProductTable.vue";
    import SearchBox from "@/components/ProductSearchBox.vue";
    import axios from "/src/utils/axios.js";
    import LoaderComp from "@/components/LoaderComp.vue";
  
    const state = reactive({
        loading: false,
        products: [],
    });
  
    async function fetchData() {
        try {
            state.loading = true;
            const response = await axios.get("/product");
            state.products = response.data;
            state.products.sort((a, b) => a.productID - b.productID);
        } catch (error) {
            console.error(error);
        } finally {
            state.loading = false;
        }
    }
  
    async function searchByName(name) {
        try {
            state.loading = true;
            const response = await axios.get(
                `/product/GetByName/?name=${name}`
            );
            state.products = response.data;
        } catch (error) {
            console.error(error);
        } finally {
            state.loading = false;
        }
    }

    async function searchByCategory(category) {
        try {
            state.loading = true;
            const response = await axios.get(
                `/product/GetByCategoryType/?category=${category}`
            );
            state.products = response.data;
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
    .product-page {
        width: 75%;
        margin-top: 80px;
        margin-bottom: 50px;
        display: flex;
        flex-direction: column;
        align-items: flex-start;
    }
</style>