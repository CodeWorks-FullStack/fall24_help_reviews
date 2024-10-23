

namespace help_reviews.Services;

public class RestaurantsService
{

  private readonly RestaurantsRepository _rr;

  public RestaurantsService(RestaurantsRepository rr)
  {
    _rr = rr;
  }

  public List<Restaurant> GetRestaurants()
  {
    return _rr.GetRestaurants();
  }

  internal Restaurant CreateRestaurant(RestaurantCreationDTO creationData, Account userInfo)
  {

    // TODO FUTURE FEATURE Only certain accounts can create restaurants


    return _rr.CreateRestaurant(creationData, userInfo.Id);
  }

  internal Restaurant Get(int restaurantId)
  {

    Restaurant restaurant = _rr.Get(restaurantId);

    // Might want to null check

    return restaurant;
  }
}
