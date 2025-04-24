using MediatR;
using AllTheBeans.Core.ViewModels;

public record GetBeanByIdQuery(Guid Id) : IRequest<BeanViewModel?>;