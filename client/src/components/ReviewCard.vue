<script setup>
import { Review } from '@/models/Review.js';

defineProps({
  review: { type: Review, required: true }
})
</script>


<template>
  <div class="bg-light shadow p-3">
    <div class="d-flex justify-content-between">
      <div>
        <a v-if="review.imgUrl" :href="review.imgUrl" target="_blank" title="Picture of disgust">
          <i class="mdi mdi-camera text-success fs-3"></i>
        </a>
      </div>
      <h3 class="text-success">"{{ review.title }}"</h3>
      <div class="d-flex gap-1">
        <div class="text-end">
          <p class="fw-bold mb-0">{{ review.reviewer.name }}</p>
          <time>{{ review.createdAt.toLocaleDateString() }}</time>
        </div>
        <img :src="review.reviewer.picture" :alt="review.reviewer.name" class="reviewer-picture">
      </div>
    </div>
    <p>{{ review.body }}</p>
  </div>
</template>


<style lang="scss" scoped>
.reviewer-picture {
  width: 5em;
  aspect-ratio: 1/1;
  border-radius: 50%;
  object-fit: cover;
}
</style>