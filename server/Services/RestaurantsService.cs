


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

    // Might want to null check ✅
    if (restaurant == null) throw new Exception($"Invalid id: {restaurantId}");

    return restaurant;
  }

  internal Restaurant ShutdownRestaurant(int restaurantId, string userId)
  {
    Restaurant restaurantToShutdown = Get(restaurantId);

    if (restaurantToShutdown.CreatorId != userId) throw new Exception("NOT YOUR RESTAURANT, BIG DAWG");

    restaurantToShutdown.IsShutdown = !restaurantToShutdown.IsShutdown;

    _rr.ShutdownRestaurant(restaurantToShutdown);

    return restaurantToShutdown;
  }

}
