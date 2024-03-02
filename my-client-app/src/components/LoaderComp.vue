<template>
	<div v-if="loading" class="loader-container">
		<div>
			<div class="loader"></div>
			Loading...
		</div>
	</div>
</template>

<script setup>
	import { defineProps } from "vue";

	defineProps({
		loading: Boolean,
	});
</script>
