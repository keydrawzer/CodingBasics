<template>
	<div class="top-bar">
		<router-link to="/" class="nav-link">
			<div class="icon-container">
				<i class="material-icons">home</i>
				<span>Home</span>
			</div>
		</router-link>
		<router-link to="/persons" class="nav-link">
			<div class="icon-container">
				<i class="material-icons">people</i>
				<span>Persons</span>
			</div>
		</router-link>
		<router-link to="/products" class="nav-link">
			<div class="icon-container">
				<i class="material-icons">shopping_bag</i>
				<span>Products</span>
			</div>
		</router-link>
		<router-link to="/sales" class="nav-link">
			<div class="icon-container">
				<i class="material-icons">attach_money</i>
				<span>Sales</span>
			</div>
		</router-link>
	</div>
</template>

<script setup></script>

<style scoped>
	.top-bar {
		background-color: #181723;
		color: #fff;
		padding: 10px;
		position: fixed;
		width: 100%;
		top: 0;
		display: flex;
		justify-content: flex-start;
		align-items: center;
	}

	.nav-link {
		text-decoration: solid;
		font-weight: 500;
		color: #fff;
		display: flex;
		flex-direction: column;
		align-items: center;
		margin-left: 25px;
	}

	.icon-container {
		display: flex;
		align-items: center;
	}

	.material-icons {
		font-size: 24px;
		margin-right: 8px;
	}
</style>
