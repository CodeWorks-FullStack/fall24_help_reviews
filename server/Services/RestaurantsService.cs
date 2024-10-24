


namespace help_reviews.Services;

public class RestaurantsService
{

  private readonly RestaurantsRepository _rr;

  public RestaurantsService(RestaurantsRepository rr)
  {
    _rr = rr;
  }

  private List<Restaurant> GetRestaurants()
  {
    List<Restaurant> restaurants = _rr.GetRestaurants();

    return restaurants;
  }

  public List<Restaurant> GetRestaurants(string userId)
  {
    List<Restaurant> restaurants = GetRestaurants();
    // return restaurants.FindAll(restaurant => restaurant.IsShutdown == false);
    // NOTE findAll works very similar to filter in js
    return restaurants.FindAll(restaurant => !restaurant.IsShutdown || restaurant.CreatorId == userId);
  }

  internal Restaurant CreateRestaurant(RestaurantCreationDTO creationData, Account userInfo)
  {

    // TODO FUTURE FEATURE Only certain accounts can create restaurants


    return _rr.CreateRestaurant(creationData, userInfo.Id);
  }

  private Restaurant GetById(int restaurantId)
  {

    Restaurant restaurant = _rr.Get(restaurantId);

    // Might want to null check ✅
    if (restaurant == null) throw new Exception($"Invalid id: {restaurantId}");

    return restaurant;
  }

  internal Restaurant GetById(int restaurantId, string userId)
  {
    Restaurant restaurant = GetById(restaurantId);

    if (restaurant.IsShutdown && restaurant.CreatorId != userId)
    {
      throw new Exception("NOT YOUR RESTAURANT");
    }

    return restaurant;
  }



  internal Restaurant ShutdownRestaurant(int restaurantId, string userId)
  {
    Restaurant restaurantToShutdown = GetById(restaurantId);

    if (restaurantToShutdown.CreatorId != userId) throw new Exception("NOT YOUR RESTAURANT, BIG DAWG");

    restaurantToShutdown.IsShutdown = !restaurantToShutdown.IsShutdown;

    _rr.ShutdownRestaurant(restaurantToShutdown);

    return restaurantToShutdown;
  }


}
