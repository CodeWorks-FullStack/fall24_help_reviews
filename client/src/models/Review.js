import { DBItem } from "./DBItem.js";
import { Profile } from "./Profile.js";
import { Restaurant } from "./Restaurant.js";

export class Review extends DBItem {
  constructor(data) {
    super(data)
    this.title = data.title
    this.body = data.body
    this.imgUrl = data.imgUrl
    this.creatorId = data.creatorId
    this.restaurantId = data.restaurantId
    this.reviewer = data.reviewer ? new Profile(data.reviewer) : null
    this.restaurant = data.restaurant ? new Restaurant(data.restaurant) : null
  }
}
