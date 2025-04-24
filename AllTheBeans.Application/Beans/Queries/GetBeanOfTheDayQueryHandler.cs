using AllTheBeans.Core.Interfaces;
using AllTheBeans.Core.ViewModels;
using MediatR;
using AllTheBeans.Application.Beans.Queries;

public class GetBeanOfTheDayQueryHandler : IRequestHandler<GetBeanOfTheDayQuery, BeanViewModel?>
{
    private readonly IBeanRepository _beanRepository;

    public GetBeanOfTheDayQueryHandler(IBeanRepository beanRepository)
    {
        _beanRepository = beanRepository;
    }

    public async Task<BeanViewModel?> Handle(GetBeanOfTheDayQuery request, CancellationToken cancellationToken)
    {
        var bean = await _beanRepository.GetBeanOfTheDayAsync();

        if (bean == null)
            return null;

        var viewModel = new BeanViewModel
        {
            Id = bean.Id,
            Name = bean.Name,
            Description = bean.Description,
            Cost = bean.Cost,
            Colour = bean.Colour,
            Country = bean.Country.Name,
            ImageUrl = bean.Image.Url,
            IsBeanOfTheDay = bean.IsBeanOfTheDay
        };

        return viewModel;
    }
}
