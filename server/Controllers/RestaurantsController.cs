namespace help_reviews.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class RestaurantsController : ControllerBase
{

  private readonly RestaurantsService _rs;
  private readonly RestaurantsReviewsService _reviewsService;
  private readonly Auth0Provider _auth0Provider;

  public RestaurantsController(RestaurantsService rs, Auth0Provider auth0Provider, RestaurantsReviewsService reviewsService)
  {
    _rs = rs;
    _auth0Provider = auth0Provider;
    _reviewsService = reviewsService;
  }

  // NOTE you do not have to be logged in to access this route
  [HttpGet]
  public async Task<ActionResult<List<Restaurant>>> GetRestaurants()
  {

    try
    {
      // NOTE we can still see who is making the request
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      // NOTE make sure to add elvis operator if route is not authorized, because userInfo will be null
      List<Restaurant> restaurants = _rs.GetRestaurants(userInfo?.Id);
      return Ok(restaurants);
    }
    catch (System.Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Restaurant>> CreateRestaurant([FromBody] RestaurantCreationDTO creationData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

      Restaurant restaurant = _rs.CreateRestaurant(creationData, userInfo);

      return Ok(restaurant);

    }
    catch (System.Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpGet("{restaurantId}")]
  public async Task<ActionResult<Restaurant>> GetRestaurant(int restaurantId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Restaurant restaurant = _rs.GetById(restaurantId, userInfo?.Id);

      return Ok(restaurant);
    }
    catch (System.Exception e)
    {
      return BadRequest(e.Message);
    }
  }




  [HttpGet("{restaurantId}/reviews")]
  public ActionResult<List<RestaurantReview>> GetReviews(int restaurantId)
  {
    try
    {
      List<RestaurantReview> reviews = _reviewsService.GetReviews(restaurantId);

      return Ok(reviews);

    }
    catch (System.Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpDelete("{restaurantId}")]
  public async Task<ActionResult<Restaurant>> ShutdownRestaurant(int restaurantId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Restaurant restaurant = _rs.ShutdownRestaurant(restaurantId, userInfo.Id);
      return Ok(restaurant);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }



}
