

namespace help_reviews.Services;

public class RestaurantsReviewsService
{

  private readonly RestaurantsReviewsRepository _rrr;

  public RestaurantsReviewsService(RestaurantsReviewsRepository rrr)
  {
    _rrr = rrr;
  }

  internal RestaurantReview Create(RestaurantReviewCreationDTO creationData, Account userInfo)
  {

    // TODO PROBABLY SHOULD MAKE SURE THE Restaurant is not shutdown

    return _rrr.Create(creationData, userInfo.Id);
  }

  internal List<RestaurantReview> GetReviews(int restaurantId)
  {
    return _rrr.GetRestaurantReviews(restaurantId);
  }


  internal List<RestaurantReview> GetReviews(string creatorId)
  {
    return _rrr.GetRestaurantReviews(creatorId);
  }



}
