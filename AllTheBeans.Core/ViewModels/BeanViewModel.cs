namespace AllTheBeans.Core.ViewModels;

public class BeanViewModel
{
    public Guid Id { get; set; }
    public string ImportId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Cost { get; set; }
    public string Colour { get; set; } = string.Empty;
    public bool IsBeanOfTheDay { get; set; }
    public string Country { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}
