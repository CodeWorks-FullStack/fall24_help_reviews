import { AppState } from "@/AppState.js";
import { api } from "./AxiosService.js";
import { logger } from "@/utils/Logger.js";
import { Restaurant } from "@/models/Restaurant.js";

class RestaurantsService {
  async getRestaurants() {
    try {
      const response = await api.get("api/restaurants");
      AppState.restaurants = response.data.map(pojo => new Restaurant(pojo));
    } catch (error) {
      logger.error(error);
    }
  }

  async getRestaurantById(id) {
    try {
      // NOTE this is a way to prevent the user from seeing ghost data
      AppState.restaurant = null;
      const response = await api.get(`api/restaurants/${id}`);
      AppState.restaurant = new Restaurant(response.data);
    } catch (error) {
      logger.error(error);
    }
  }
}


export const restaurantsService = new RestaurantsService();