import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Review } from "@/models/Review.js"

class ReviewsService {
  async createReview(reviewData) {
    const response = await api.post('api/reviews', reviewData)
    logger.log('CREATED REVIEW', response.data)
    const newReview = new Review(response.data)
    AppState.reviews.push(newReview)
    const restaurantThatReceicedReport = AppState.restaurants.find(restaurant => restaurant.id == newReview.restaurantId)
    restaurantThatReceicedReport.reviewCount++
  }
  async getRestaurantReviews(restaurantId) {
    const response = await api.get(`api/restaurants/${restaurantId}/reviews`)
    logger.log(response.data)
    AppState.reviews = response.data.map(pojo => new Review(pojo))
  }

}

export const reviewsService = new ReviewsService()