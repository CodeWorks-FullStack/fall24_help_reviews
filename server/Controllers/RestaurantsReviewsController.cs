namespace help_reviews.Controllers;

[ApiController]
[Route("/api/reviews")]
public class RestaurantsReviewsController : ControllerBase
{

  private readonly RestaurantsReviewsService _rrs;
  private readonly Auth0Provider _auth;


  public RestaurantsReviewsController(RestaurantsReviewsService rs, Auth0Provider auth)
  {
    _rrs = rs;
    _auth = auth;
  }


  [HttpPost]
  [Authorize]
  public async Task<ActionResult<RestaurantReview>> CreateRestaurantReview([FromBody] RestaurantReviewCreationDTO creationData)
  {
    try
    {

      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);

      RestaurantReview review = _rrs.Create(creationData, userInfo);

      return Ok(review);

    }
    catch (System.Exception e)
    {
      return BadRequest(e.Message);
    }
  }


}
