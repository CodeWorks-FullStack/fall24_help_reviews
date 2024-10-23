<script setup>
import { AppState } from '@/AppState.js';
import { restaurantsService } from '@/services/RestaurantsService.js';
import Pop from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';

const restaurant = computed(() => AppState.restaurant)
const route = useRoute()

async function getRestaurant() {
  try {
    await restaurantsService.getRestaurantById(route.params.restaurantId)
  }
  catch (error) {
    Pop.error(error);
  }
}

onMounted(() => {
  getRestaurant()
})


</script>

<template>
  <div class="container restaurant" v-if="restaurant">
    <div class="row">
      <div class="col-lg-10 m-auto">
        <div class="text-center py-2">
          <img class="img-fluid __img" :src="restaurant.imgUrl" alt="Restaurant Picture">
          <h1>{{ restaurant.name }}</h1>
        </div>
      </div>
    </div>
  </div>
  <div v-else>loading...</div>
</template>

<style scoped lang="scss">
.restaurant {

  .__img {
    max-height: 60vh;
    object-fit: cover;
  }


}
</style>
