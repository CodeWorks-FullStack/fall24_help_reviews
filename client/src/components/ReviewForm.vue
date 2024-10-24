<script setup>
import { AppState } from '@/AppState.js';
import { reviewsService } from '@/services/ReviewsService.js';
import Pop from '@/utils/Pop.js';
import { computed, ref } from 'vue';


const restaurants = computed(() => AppState.restaurants)
const selectedRestaurant = computed(() => AppState.restaurants.find(restaurant => restaurant.id == editableReviewData.value.restaurantId))

const editableReviewData = ref({
  title: '',
  body: '',
  imgUrl: '',
  restaurantId: 0
})

async function createReview() {
  try {
    await reviewsService.createReview(editableReviewData.value)
    editableReviewData.value = {
      title: '',
      body: '',
      imgUrl: '',
      restaurantId: 0
    }
  }
  catch (error) {
    Pop.error(error);
  }
}

</script>


<template>
  <form @submit.prevent="createReview()">
    <div class="row">
      <div class="col-md-6 mb-3 mb-md-0">
        <img v-if="selectedRestaurant" :src="selectedRestaurant.imgUrl" :alt="selectedRestaurant.name">
        <img v-else src="https://placehold.co/600x400" alt="Placeholder">
      </div>
      <div class="col-md-6 mb-3 mb-md-0">
        <select v-model="editableReviewData.restaurantId" class="form-select" aria-label="Select a restaurant" required>
          <option selected :value="0" disabled>Select a restaurant</option>
          <option v-for="restaurant in restaurants" :key="restaurant.id" :value="restaurant.id">
            {{ restaurant.name }}
          </option>
        </select>
      </div>
    </div>
    <div class="mb-3">
      <label for="review-title" class="form-label">Title for Review</label>
      <input v-model="editableReviewData.title" type="text" class="form-control" id="review-title"
        aria-describedby="review-title-help" required maxlength="255">
      <div id="review-title-help" class="form-text">make it eye-catching...</div>
    </div>
    <div class="mb-3">
      <label for="review-img" class="form-label">Img for Review</label>
      <input v-model="editableReviewData.imgUrl" type="url" class="form-control" id="review-img"
        aria-describedby="review-img-help" required maxlength="255">
      <div id="review-img-help" class="form-text">make it pretty...</div>
    </div>
    <div class="mb-3">
      <label for="review-details" class="form-label">Review Details</label>
      <textarea v-model="editableReviewData.body" class="form-control" id="review-details" rows="3"
        aria-describedby="review-details-help" required maxlength="3000"></textarea>
      <div id="review-details-help" class="form-text">make it juicy...</div>
    </div>
    <div class="text-end">
      <button class="btn btn-success" type="submit">Submit</button>
    </div>
  </form>
</template>


<style lang="scss" scoped>
img {
  height: 20dvh;
  width: 100%;
  object-fit: cover;
}
</style>