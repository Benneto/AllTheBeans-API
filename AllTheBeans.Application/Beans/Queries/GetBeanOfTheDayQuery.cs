using AllTheBeans.Core.Common;
using AllTheBeans.Core.ViewModels;
using MediatR;

namespace AllTheBeans.Application.Beans.Queries
{
    public class GetBeanOfTheDayQuery : IRequest<BeanViewModel>
    {
    }
}
