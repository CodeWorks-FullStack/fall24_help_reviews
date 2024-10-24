import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"

class ReviewsService {
  async getRestaurantReviews(restaurantId) {
    const response = await api.get(`api/restaurants/${restaurantId}/reviews`)
    logger.log(response.data)
  }

}

export const reviewsService = new ReviewsService()