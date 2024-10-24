import { AppState } from "@/AppState.js";
import { api } from "./AxiosService.js";
import { logger } from "@/utils/Logger.js";
import { Restaurant } from "@/models/Restaurant.js";

class RestaurantsService {
  async shutdownRestaurant(restaurantId) {
    const response = await api.delete(`api/restaurants/${restaurantId}`)
    const newRestaurant = new Restaurant(response.data)
    AppState.restaurant = newRestaurant
    const index = AppState.restaurantsForReview.findIndex(r => r.id == newRestaurant.id)
    AppState.restaurantsForReview.splice(index, 1, newRestaurant)
  }
  async getRestaurantsForReviews() {
    // NOTE it would be cool if I had a dedicated endpoint that did something similar for me 😉
    const response = await api.get("api/restaurants");
    AppState.restaurantsForReview = response.data.map(pojo => new Restaurant(pojo))
  }
  async getRestaurants() {
    const response = await api.get("api/restaurants");
    AppState.restaurants = response.data.map(pojo => new Restaurant(pojo));
  }

  async getRestaurantById(id) {
    // NOTE this is a way to prevent the user from seeing ghost data
    AppState.restaurant = null;
    const response = await api.get(`api/restaurants/${id}`);
    AppState.restaurant = new Restaurant(response.data);
  }
}


export const restaurantsService = new RestaurantsService();