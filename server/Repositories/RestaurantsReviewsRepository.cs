namespace help_reviews.Repositories;

public class RestaurantsReviewsRepository
{

  private readonly IDbConnection _db;

  public RestaurantsReviewsRepository(IDbConnection db)
  {
    _db = db;
  }

  public RestaurantReview Create(RestaurantReviewCreationDTO creationData, string creatorId)
  {
    string sql = @"
      INSERT INTO restaurant_reviews(title, body, imgUrl, restaurantId, creatorId)
      VALUES(@Title, @Body, @ImgUrl, @RestaurantId, @creatorId);

      SELECT * 
        FROM 
          restaurant_reviews
        JOIN 
          accounts ON accounts.id = @creatorId
        WHERE 
          restaurant_reviews.id = LAST_INSERT_ID()
    ;";


    return _db.Query(sql, (RestaurantReview r, Profile p) =>
    {
      r.Reviewer = p;
      return r;
    }, new
    {
      creationData.Title,
      creationData.Body,
      creationData.ImgUrl,
      creationData.RestaurantId,
      creatorId
    }).FirstOrDefault();


  }


  // REVIEW THIS IS VERY RELEVANT FOR THE FINAL
  public List<RestaurantReview> GetRestaurantReviews(int restaurantId)
  {
    string sql = @"
      SELECT * FROM 
        restaurant_reviews
      JOIN 
        accounts on accounts.id = restaurant_reviews.creatorId
      WHERE 
        restaurant_reviews.restaurantId = @restaurantId
    ;";

    return _db.Query(sql, (RestaurantReview r, Profile p) =>
    {
      r.Reviewer = p;
      return r;
    }, new
    {
      restaurantId
    }).ToList();

  }
  public List<RestaurantReview> GetRestaurantReviews(string creatorId)
  {
    string sql = @"
      SELECT * FROM 
        restaurant_reviews
      JOIN
        restaurants ON restaurants.id = restaurant_reviews.restaurantId
      WHERE 
        restaurant_reviews.creatorId = @creatorId
    ;";

    return _db.Query(sql, (RestaurantReview review, Restaurant restaurant) =>
    {
      review.Restaurant = restaurant;
      return review;
    }, new
    {
      creatorId
    }).ToList();
  }

}

