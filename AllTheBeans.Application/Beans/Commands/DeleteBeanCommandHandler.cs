using AllTheBeans.Core.Common;
using AllTheBeans.Core.Interfaces;
using MediatR;

public class DeleteBeanCommandHandler : IRequestHandler<DeleteBeanCommand, Result<Guid>>
{
    private readonly IBeanRepository _beanRepository;
    private readonly IBeanImageRepository _beanImageRepository;

    public DeleteBeanCommandHandler(
        IBeanRepository beanRepository,
        IBeanImageRepository beanImageRepository)
    {
        _beanRepository = beanRepository;
        _beanImageRepository = beanImageRepository;
    }


    public async Task<Result<Guid>> Handle(DeleteBeanCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var bean = await _beanRepository.GetByIdAsync(request.Id);

            if (bean == null)
                return Result<Guid>.Failure("Bean not found.");

            var image = await _beanImageRepository.GetByIdAsync(bean.ImageId);
            if (image == null)
            {
                throw new Exception("Image not found");
            }

            await _beanImageRepository.DeleteAsync(image.Id);
            await _beanRepository.DeleteAsync(bean.Id);

            return Result<Guid>.Success(bean.Id);
        }
        catch (Exception)
        {
            return Result<Guid>.Error("An unexpected error occurred while deleting the bean.");
        }
    }
}
