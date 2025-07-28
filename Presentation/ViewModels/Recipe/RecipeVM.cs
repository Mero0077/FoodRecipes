namespace Presentation.ViewModels.Recipe
{
    public class RecipeVM
    {
      public string Name { get; set; }
      public string Description { get; set; }
      public decimal Price { get; set; }
      public Guid CategoryId { get; set; }
      public string? Tag { get; set; }
      public string? ImageUrl { get; set; }


    }
}
