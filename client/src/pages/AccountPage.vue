<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import ReviewCard from '@/components/ReviewCard.vue';

const account = computed(() => AppState.account)
const reviews = computed(() => AppState.myReviews)
</script>

<template>
  <div v-if="account" class="container">
    <div class="row">
      <div class="col-12">
        <h1 class="text-center">Your Reviews</h1>
      </div>
    </div>
    <div class="row">
      <div v-for="review in reviews" :key="review.id" class="col-10 m-auto mb-3">
        <ReviewCard :review="review" />
      </div>
    </div>
  </div>
  <div v-else class="container">
    <h1>Loading... <i class="mdi mdi-loading mdi-spin"></i></h1>
  </div>
</template>

<style scoped lang="scss"></style>
