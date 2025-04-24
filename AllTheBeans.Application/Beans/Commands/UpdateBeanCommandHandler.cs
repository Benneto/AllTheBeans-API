using AllTheBeans.Application.Beans.Commands;
using AllTheBeans.Core.Common;
using AllTheBeans.Core.Entities;
using AllTheBeans.Core.Interfaces;
using MediatR;

public class UpdateBeanCommandHandler : IRequestHandler<UpdateBeanCommand, Result<Guid>>
{
    private readonly IBeanRepository _beanRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IBeanImageRepository _beanImageRepository;

    public UpdateBeanCommandHandler(
        IBeanRepository beanRepository,
        ICountryRepository countryRepository,
        IBeanImageRepository beanImageRepository)
    {
        _beanRepository = beanRepository;
        _countryRepository = countryRepository;
        _beanImageRepository = beanImageRepository;
    }

    public async Task<Result<Guid>> Handle(UpdateBeanCommand request, CancellationToken cancellationToken)
    {
        var validationResult = ValidateCommand(request);

        if (validationResult.Status != ResultStatus.Success)
        {
            return validationResult;
        }

        try
        {
            var bean = await _beanRepository.GetByIdAsync(request.Id);

            if (bean == null)
                return Result<Guid>.Failure("Bean not found.");

            var image = await _beanImageRepository.GetByIdAsync(bean.ImageId);
            if (image == null)
                throw new Exception("Image associated with bean not found.");

            image.Url = request.ImageUrl;
            await _beanImageRepository.UpdateAsync(image);

            var country = await _countryRepository.GetByNameAsync(request.CountryName);
            if (country == null)
            {
                country = await _countryRepository.CreateAsync(new Country
                {
                    Id = Guid.NewGuid(),
                    Name = request.CountryName
                });
            }

            bean.Name = request.Name;
            bean.Description = request.Description;
            bean.Cost = request.Cost;
            bean.Colour = request.Colour;
            bean.IsBeanOfTheDay = request.IsBeanOfTheDay;
            bean.CountryId = country.Id;

            await _beanRepository.UpdateAsync(bean);

            return Result<Guid>.Success(bean.Id);
        }
        catch (Exception ex)
        {
            return Result<Guid>.Error("An unexpected error occurred while updating the bean.");
        }
    }

    private static Result<Guid> ValidateCommand(UpdateBeanCommand request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return Result<Guid>.Failure("Name is required.");

        if (string.IsNullOrWhiteSpace(request.Description))
            return Result<Guid>.Failure("Description is required.");

        if (string.IsNullOrWhiteSpace(request.Colour))
            return Result<Guid>.Failure("Colour is required.");

        if (string.IsNullOrWhiteSpace(request.CountryName))
            return Result<Guid>.Failure("Country name is required.");

        if (string.IsNullOrWhiteSpace(request.ImageUrl))
            return Result<Guid>.Failure("Image URL is required.");

        if (request.Cost <= 0)
            return Result<Guid>.Failure("Cost must be greater than zero.");

        return Result<Guid>.Success();
    }
}
