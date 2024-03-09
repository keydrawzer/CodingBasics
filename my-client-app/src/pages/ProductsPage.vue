<template>
	<div class="products-page">
		<h1>List of Products</h1>
        <ProductTable/>
	</div>
</template>

<script setup>
    //import { onMounted, reactive } from "vue";
    import ProductTable from "@/components/ProductTable.vue";
</script>

<style scoped>
    .products-page {
        width: 75%;
        margin-top: 80px;
        margin-bottom: 50px;
        display: flex;
        flex-direction: column;
        align-items: flex-start;
    }
</style>
