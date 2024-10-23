using help_reviews.interfaces;

// INPUT MODEL
public class RestaurantCreationDTO
{
  public string Name { get; set; }
  public string Description { get; set; }
  public string ImgUrl { get; set; }
}


// RETURN OBJECT
public class Restaurant : IRepoItem<int>
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }

  public string Name { get; set; }
  public string Description { get; set; }
  public string ImgUrl { get; set; }
  public int Visits { get; set; }
  public bool IsShutdown { get; set; }


  // RELATIONSHIP PROPERTIES

  public string CreatorId { get; set; }
  public int ReviewCount { get; set; }

  public Profile Owner { get; set; }

}