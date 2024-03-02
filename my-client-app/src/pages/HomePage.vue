<template>
	<div class="home">
		<img alt="Vue logo" src="../assets/logo.png" />
		<HelloWorld msg="Welcome to Your App" />
	</div>
</template>

<script setup>
	import HelloWorld from "../components/HelloWorld.vue";
</script>
<style>
	.home {
		display: flex;
		flex-direction: column;
		align-items: center;
		justify-content: center;
		min-height: 100vh;
	}
</style>
