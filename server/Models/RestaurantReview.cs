using System.ComponentModel.DataAnnotations;
using help_reviews.interfaces;

// NOTE generally abstract all DTO's
public class RestaurantReviewCreationDTO
{
  public string Title { get; set; }
  public string Body { get; set; }
  [MaxLength(3000)]
  public string ImgUrl { get; set; }
  public int RestaurantId { get; set; }
}


public class RestaurantReview : IRepoItem<int>
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public string Title { get; set; }
  public string Body { get; set; }
  public string ImgUrl { get; set; }


  // RELATIONSHIP PROPERTIES
  public string CreatorId { get; set; }
  public int RestaurantId { get; set; }

  public Profile Reviewer { get; set; }
  public Restaurant Restaurant { get; set; }


}
