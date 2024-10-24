<script setup>
import { AppState } from '@/AppState.js';
import { restaurantsService } from '@/services/RestaurantsService.js';
import Pop from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';
import RestaurantCard from '../components/RestaurantCard.vue'

const restaurants = computed(() => AppState.restaurants)

async function getRestaurants() {
  try {
    await restaurantsService.getRestaurants()
  }
  catch (error) {
    Pop.error(error);
  }
}

onMounted(() => {
  getRestaurants()
})


</script>

<template>
  <div class="container">

    <div class="restaurants row">
      <div class="col-md-4 mb-3" v-for="r in restaurants" :key="r.id">
        <RestaurantCard :restaurant="r" />
      </div>
    </div>


  </div>
</template>

<style scoped lang="scss"></style>
