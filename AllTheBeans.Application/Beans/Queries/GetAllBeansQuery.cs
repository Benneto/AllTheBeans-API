using MediatR;
using AllTheBeans.Core.ViewModels;

public record GetAllBeansQuery() : IRequest<List<BeanViewModel>>;
