import { AppState } from "@/AppState.js";
import { api } from "./AxiosService.js";
import { logger } from "@/utils/Logger.js";
import { Restaurant } from "@/models/Restaurant.js";

class RestaurantsService {
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