<script setup>
import { AppState } from '@/AppState.js';
import { restaurantsService } from '@/services/RestaurantsService.js';
import { reviewsService } from '@/services/ReviewsService.js';
import { logger } from '@/utils/Logger.js';
import Pop from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';

const restaurant = computed(() => AppState.restaurant)
const reviews = computed(() => AppState.reviews)
const account = computed(() => AppState.account)
const route = useRoute()

async function getRestaurant() {
  try {
    await restaurantsService.getRestaurantById(route.params.restaurantId)
  }
  catch (error) {
    Pop.error(error);
  }
}

async function getRestaurantReviews() {
  try {
    await reviewsService.getRestaurantReviews(route.params.restaurantId)
  } catch (error) {
    Pop.error(error)
    logger.error(error)
  }
}

onMounted(() => {
  getRestaurant()
  getRestaurantReviews()
})


</script>

<template>
  <div class="container restaurant" v-if="restaurant">
    <div class="row">
      <div class="col-lg-10 m-auto mb-3">
        <div class="py-2">
          <div class="d-flex justify-content-between">
            <h1 class="text-success">{{ restaurant.name }}</h1>
            <div v-if="restaurant.isShutdown" class="bg-danger fs-2 text-light px-4">
              <i class="mdi mdi-close-circle-outline"></i>
              CURRENTLY SHUTDOWN
            </div>
          </div>
          <div class="shadow bg-light">
            <img class="img-fluid restaurant-img" :src="restaurant.imgUrl" alt="Restaurant Picture">
            <div class="p-4">
              <p class="restaurant-description">{{ restaurant.description }}</p>
              <div class="d-flex justify-content-between">
                <div class="d-flex gap-5">
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
                <div v-if="restaurant.creatorId == account?.id" class="d-flex gap-5">
                  <button class="btn btn-success fs-5 fw-bold">
                    <i class="mdi mdi-door"></i>
                    Shut Down
                  </button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="col-lg-8 m-auto">
        <h2>Reports for <span class="text-success">{{ restaurant.name }}</span></h2>
        {{ reviews }}
      </div>
    </div>
  </div>
  <div v-else>loading...</div>
</template>

<style scoped lang="scss">
.restaurant {

  .restaurant-img {
    max-height: 40dvh;
    width: 100%;
    object-fit: cover;
  }


}

.restaurant-description {
  min-height: 12dvh;
}
</style>
