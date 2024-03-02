<template>
	<TopBar />
	<router-view />
</template>

<script setup>
	import TopBar from "./components/TopBar.vue";
	import { onMounted } from "vue";

	onMounted(() => {
		console.log("Component is mounted");
	});
</script>

<style>
	#app {
		display: flex;
		flex-direction: column;
		align-items: center;
		min-height: 100vh;
		font-family: Avenir, Helvetica, Arial, sans-serif;
		-webkit-font-smoothing: antialiased;
		-moz-osx-font-smoothing: grayscale;
		text-align: center;
		color: #2c3e50;
	}
</style>
