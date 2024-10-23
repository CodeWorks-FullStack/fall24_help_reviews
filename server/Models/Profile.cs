using help_reviews.interfaces;

namespace help_reviews.Models;

public class Profile : IRepoItem<string>
{
  public string Id { get; set; }
  public string Name { get; set; }
  public string Picture { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}
