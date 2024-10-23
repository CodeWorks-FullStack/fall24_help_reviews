<script setup>
const props = defineProps({
	restaurant: { type: Object, required: true }
})
</script>

<template>
	<router-link :to="{
		name: 'Restaurant',
		params: { restaurantId: restaurant.id }
	}">
		<div class="card" :key="restaurant.id">
			<img :src="restaurant.imgUrl" alt="restaurant picture">
			<div class="card-body">
				<h3>{{ restaurant.name }}</h3>
			</div>
		</div>
	</router-link>
</template>
