import { DBItem } from "./DBItem.js"
import { Profile } from "./Profile.js"

export class Restaurant extends DBItem {
  constructor(data) {
    super(data)
    this.name = data.name
    this.description = data.description
    this.imgUrl = data.imgUrl
    this.visits = data.visits
    this.isShutdown = data.isShutdown
    this.creatorId = data.creatorId
    this.reviewCount = data.reviewCount
    this.owner = data.owner ? new Profile(data.owner) : null
  }
}
