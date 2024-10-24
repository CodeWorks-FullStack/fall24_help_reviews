

namespace help_reviews.Services;

public class RestaurantsReviewsService
{

  private readonly RestaurantsReviewsRepository _rrr;
  private readonly RestaurantsService _restaurantsService;

  public RestaurantsReviewsService(RestaurantsReviewsRepository rrr, RestaurantsService restaurantsService)
  {
    _rrr = rrr;
    _restaurantsService = restaurantsService;
  }

  internal RestaurantReview Create(RestaurantReviewCreationDTO creationData, Account userInfo)
  {
    // TODO PROBABLY SHOULD MAKE SURE THE Restaurant is not shutdown
    Restaurant restaurant = _restaurantsService.GetById(creationData.RestaurantId, userInfo.Id);

    if (restaurant.IsShutdown)
    {
      throw new Exception("PLEASE RE-OPEN YOUR RESTAURANT BEFORE LEAVING REVIEWS");
    }

    return _rrr.Create(creationData, userInfo.Id);
  }

  private List<RestaurantReview> GetReviews(int restaurantId)
  {

    return _rrr.GetRestaurantReviews(restaurantId);
  }

  internal List<RestaurantReview> GetReviews(int restaurantId, string userId)
  {
    // TODO PROBABLY SHOULD MAKE SURE THE Restaurant is not shutdown
    // NOTE the other service performs all needed checks
    _restaurantsService.GetById(restaurantId, userId);
    return GetReviews(restaurantId);
  }


  internal List<RestaurantReview> GetReviews(string creatorId)
  {
    return _rrr.GetRestaurantReviews(creatorId);
  }


}
