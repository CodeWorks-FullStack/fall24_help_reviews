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

  [HttpGet]
  public ActionResult<List<Restaurant>> GetRestaurants()
  {

    try
    {
      List<Restaurant> restaurants = _rs.GetRestaurants();
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
  public ActionResult<Restaurant> GetRestaurant(int restaurantId)
  {
    try
    {
      Restaurant restaurant = _rs.Get(restaurantId);

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




}
