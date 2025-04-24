using AllTheBeans.Core.Common;
using MediatR;

public class DeleteBeanCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }

    public DeleteBeanCommand(Guid id)
    {
        Id = id;
    }
}
