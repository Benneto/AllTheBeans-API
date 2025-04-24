using MediatR;
using AllTheBeans.Core.Interfaces;
using AllTheBeans.Core.ViewModels;

public class GetAllBeansQueryHandler : IRequestHandler<GetAllBeansQuery, List<BeanViewModel>>
{
    private readonly IBeanRepository _beanRepository;

    public GetAllBeansQueryHandler(IBeanRepository beanRepository)
    {
        _beanRepository = beanRepository;
    }

    public async Task<List<BeanViewModel>> Handle(GetAllBeansQuery request, CancellationToken cancellationToken)
    {
        var beans = await _beanRepository.GetAllAsync();

        return beans.Select(b => new BeanViewModel
        {
            Id = b.Id,
            ImportId = b.ImportId,
            Name = b.Name,
            Description = b.Description,
            Cost = b.Cost,
            Colour = b.Colour,
            IsBeanOfTheDay = b.IsBeanOfTheDay,
            Country = b.Country?.Name ?? string.Empty,
            ImageUrl = b.Image?.Url ?? string.Empty
        }).ToList();
    }
}
