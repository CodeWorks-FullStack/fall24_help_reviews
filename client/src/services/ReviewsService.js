import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Review } from "@/models/Review.js"

class ReviewsService {
  async getRestaurantReviews(restaurantId) {
    const response = await api.get(`api/restaurants/${restaurantId}/reviews`)
    logger.log(response.data)
    AppState.reviews = response.data.map(pojo => new Review(pojo))
  }

}

export const reviewsService = new ReviewsService()