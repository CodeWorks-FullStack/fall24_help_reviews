import { reactive } from 'vue'
import { Restaurant } from './models/Restaurant.js'
import { Review } from './models/Review.js'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  /**@type {import('@bcwdev/auth0provider-client').Identity} */
  identity: null,
  /** @type {import('./models/Account.js').Account} user info from the database*/
  account: null,
  /** @type {Restaurant[]} */
  restaurants: [],
  /** @type {Restaurant[]} */
  restaurantsForReview: [],
  /** @type {Restaurant} */
  restaurant: null,
  /** @type {Review[]} */
  reviews: []
})

