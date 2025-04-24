using MediatR;
using AllTheBeans.Core.Common;

namespace AllTheBeans.Application.Beans.Commands
{
    public class UpdateBeanCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Cost { get; set; }
        public string Colour { get; set; } = default!;
        public bool IsBeanOfTheDay { get; set; }

        public string CountryName { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;

    }

}
