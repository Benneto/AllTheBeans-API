using MediatR;
using AllTheBeans.Core.Interfaces;
using AllTheBeans.Core.ViewModels;

public class GetBeanByIdQueryHandler : IRequestHandler<GetBeanByIdQuery, BeanViewModel?>
{
    private readonly IBeanRepository _beanRepository;

    public GetBeanByIdQueryHandler(IBeanRepository beanRepository)
    {
        _beanRepository = beanRepository;
    }

    public async Task<BeanViewModel?> Handle(GetBeanByIdQuery request, CancellationToken cancellationToken)
    {
        var bean = await _beanRepository.GetByIdAsync(request.Id);

        if (bean == null)
            return null;

        return new BeanViewModel
        {
            Id = bean.Id,
            ImportId = bean.ImportId,
            Name = bean.Name,
            Description = bean.Description,
            Cost = bean.Cost,
            Colour = bean.Colour,
            IsBeanOfTheDay = bean.IsBeanOfTheDay,
            Country = bean.Country?.Name ?? "Unknown",
            ImageUrl = bean.Image?.Url ?? ""
        };
    }
}
