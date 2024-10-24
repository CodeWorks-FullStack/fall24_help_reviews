



namespace help_reviews.Repositories;

public class RestaurantsRepository
{

  private readonly IDbConnection _db;

  public RestaurantsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Restaurant CreateRestaurant(RestaurantCreationDTO creationData, string creatorId)
  {

    string sql = @"
      INSERT INTO restaurants(name, description, imgUrl, creatorId)
      VALUES (@Name, @Description, @ImgUrl, @creatorId);
      
      SELECT 
        restaurants.*,
        accounts.* 
      FROM
          restaurants
          JOIN accounts ON accounts.id = restaurants.`creatorId`
      WHERE 
        restaurants.id = LAST_INSERT_ID()
    ;";

    return _db.Query<Restaurant, Profile, Restaurant>(sql, (r, p) =>
    {
      r.Owner = p;
      return r;
    }, new
    {
      creationData.Name,
      creationData.Description,
      creationData.ImgUrl,
      creatorId
    }).FirstOrDefault();
  }

  internal Restaurant Get(int restaurantId)
  {
    string sql = @"
          UPDATE restaurants SET visits = visits + 1 WHERE restaurants.id = @restaurantId;
          SELECT * FROM restaurants
          JOIN accounts ON accounts.id = restaurants.creatorId
          WHERE restaurants.id = @restaurantId
        ;";

    return _db.Query(sql, (Restaurant r, Profile p) =>
    {
      r.Owner = p;
      return r;
    }, new { restaurantId }).FirstOrDefault();
  }


  internal void Update(int restaurantId, RestaurantCreationDTO updateData)
  {
    string sql = @"
      UPDATE restaurants

      SET(
        name = @Name,
        description = @Description,
        imgUrl = @ImgUrl
      )
      WHERE id = @restaurantId 
      LIMIT 1
    ;";

    _db.Execute(sql, new
    {
      restaurantId,
      updateData.Name,
      updateData.Description,
      updateData.ImgUrl
    });


  }



  internal List<Restaurant> GetRestaurants()
  {

    string sql = @"
      SELECT 
        restaurants.*, 
      COUNT(
            restaurant_reviews.`restaurantId`
          ) AS reviewCount,
      accounts.*
      FROM
          restaurants
          JOIN accounts ON accounts.id = restaurants.`creatorId`
          LEFT OUTER JOIN restaurant_reviews ON restaurant_reviews.`restaurantId` = restaurants.id
      GROUP BY
          restaurants.`id`
    ;";

    return _db.Query(sql, (Restaurant r, Profile p) =>
    {
      r.Owner = p;
      return r;
    }).ToList();

    // return _db.Query<Restaurant, Profile, Restaurant>(sql, (r, p) =>
    // {
    //   r.Owner = p;
    //   return r;
    // }).ToList();
  }

  internal void ShutdownRestaurant(Restaurant restaurantToShutdown)
  {
    string sql = "UPDATE restaurants SET isShutdown = @IsShutDown WHERE id = @Id LIMIT 1;";

    int rowsAffected = _db.Execute(sql, restaurantToShutdown);

    if (rowsAffected != 1)
    {
      throw new Exception($"{rowsAffected} rows were affected, UH OH BETTER FIX IT");
    }
  }
}

