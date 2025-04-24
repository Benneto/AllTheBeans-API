using AllTheBeans.Core.Common;
using MediatR;

namespace AllTheBeans.Application.Beans.Commands;

public class CreateBeanCommand : IRequest<Result<Guid>>
{
    public string ImportId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }
    public string Colour { get; set; }
    public bool IsBeanOfTheDay { get; set; }
    public string CountryName { get; set; }
    public string ImageUrl { get; set; }
}

