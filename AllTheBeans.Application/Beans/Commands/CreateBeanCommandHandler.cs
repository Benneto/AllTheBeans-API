using AllTheBeans.Application.Beans.Commands;
using AllTheBeans.Core.Common;
using AllTheBeans.Core.Entities;
using AllTheBeans.Core.Interfaces;
using MediatR;

public class CreateBeanCommandHandler : IRequestHandler<CreateBeanCommand, Result<Guid>>
{
    private readonly IBeanRepository _beanRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IBeanImageRepository _beanImageRepository;

    public CreateBeanCommandHandler(
        IBeanRepository beanRepository,
        ICountryRepository countryRepository,
        IBeanImageRepository beanImageRepository)
    {
        _beanRepository = beanRepository;
        _countryRepository = countryRepository;
        _beanImageRepository = beanImageRepository;
    }

    public async Task<Result<Guid>> Handle(CreateBeanCommand request, CancellationToken cancellationToken)
    {
        var validationResult = ValidateCommand(request);

        if (validationResult.Status != ResultStatus.Success)
        {
            return validationResult;
        }

        try
        {
            var country = await _countryRepository.GetByNameAsync(request.CountryName);

            if (country == null)
            {
                var newCountry = new Country
                {
                    Id = Guid.NewGuid(),
                    Name = request.CountryName
                };

                country = await _countryRepository.CreateAsync(newCountry);
            }
            ;

            var newBeanImage = new BeanImage
            {
                Id = Guid.NewGuid(),
                Url = request.ImageUrl
            };

            await _beanImageRepository.CreateAsync(newBeanImage);

            var bean = new Bean
            {
                Id = Guid.NewGuid(),
                ImportId = request.ImportId,
                Name = request.Name,
                Description = request.Description,
                Cost = request.Cost,
                Colour = request.Colour,
                IsBeanOfTheDay = request.IsBeanOfTheDay,
                CountryId = country.Id,
                ImageId = newBeanImage.Id,
            };

            await _beanRepository.CreateAsync(bean);

            return Result<Guid>.Success(bean.Id);
        }
        catch (Exception ex)
        {
            return Result<Guid>.Error("An unexpected error occurred while creating the bean.");
        }
    }

    private static Result<Guid> ValidateCommand(CreateBeanCommand request)
    {
        if (string.IsNullOrWhiteSpace(request.ImportId))
            return Result<Guid>.Failure("ImportId is required.");

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
