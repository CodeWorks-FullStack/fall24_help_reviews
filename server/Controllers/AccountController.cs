namespace help_reviews.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly RestaurantsReviewsService _reviewsService;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider, RestaurantsReviewsService reviewsService)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _reviewsService = reviewsService;
  }

  [HttpGet]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateAccount(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("reviews")]
  public async Task<ActionResult<List<RestaurantReview>>> GetReviews()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

      List<RestaurantReview> reviews = _reviewsService.GetReviews(userInfo.Id);
      return Ok(reviews);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }





}
