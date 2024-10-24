<script setup>
import { Restaurant } from '@/models/Restaurant.js';

const props = defineProps({
	restaurant: { type: Restaurant, required: true }
})

</script>

<template>
	<router-link :to="{
		name: 'Restaurant',
		params: { restaurantId: restaurant.id }
	}">
		<div class="card">
			<img :src="restaurant.imgUrl" alt="restaurant picture">
			<div class="card-body">
				<h3 class="text-success">{{ restaurant.name }}</h3>
				<p>{{ restaurant.description }}</p>
				<div class="d-flex gap-3">
					<div>
						<i class="mdi mdi-account-multiple fs-1 text-success"></i>
						<b>{{ restaurant.visits }}</b>
						<span> recent visits</span>
					</div>
					<div>
						<i class="mdi mdi-file-document fs-1 text-success"></i>
						<b>{{ restaurant.reviewCount }}</b>
						<span> reviews</span>
					</div>
				</div>
			</div>
		</div>
	</router-link>
</template>


<style scoped lang="scss">
img {
	height: 30dvh;
	object-fit: cover;
	object-position: bottom;
}
</style>