using AllTheBeans.Application.Beans.Commands;
using AllTheBeans.Core.Common;
using AllTheBeans.Core.Entities;
using AllTheBeans.Core.Interfaces;
using Moq;

namespace AllTheBeans.Tests.Application.Beans.Commands
{
    public class UpdateBeanCommandHandlerTests
    {
        private Mock<IBeanRepository> _beanRepositoryMock;
        private Mock<ICountryRepository> _countryRepositoryMock;
        private Mock<IBeanImageRepository> _imageRepositoryMock;
        private UpdateBeanCommandHandler _handler;

        private static readonly Guid TestBeanId = Guid.NewGuid();
        private static readonly Guid TestImageId = Guid.NewGuid();

        private static UpdateBeanCommand CreateValidCommand() => new()
        {
            Id = TestBeanId,
            Name = "Test Bean",
            Description = "Smooth and nutty",
            Colour = "Brown",
            CountryName = "Colombia",
            ImageUrl = "http://image.com/bean.png",
            IsBeanOfTheDay = false,
            Cost = 9.99m
        };

        private static Bean CreateValidBean() => new()
        {
            Id = TestBeanId,
            Name = "Original",
            Description = "Old description",
            Cost = 5.99m,
            Colour = "Dark",
            CountryId = Guid.NewGuid(),
            ImageId = TestImageId,
            IsBeanOfTheDay = false,
            ImportId = "ABC123"
        };

        [SetUp]
        public void SetUp()
        {
            _beanRepositoryMock = new Mock<IBeanRepository>();
            _countryRepositoryMock = new Mock<ICountryRepository>();
            _imageRepositoryMock = new Mock<IBeanImageRepository>();

            _handler = new UpdateBeanCommandHandler(
                _beanRepositoryMock.Object,
                _countryRepositoryMock.Object,
                _imageRepositoryMock.Object
            );
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenBeanNotFound()
        {
            var command = CreateValidCommand();
            _beanRepositoryMock.Setup(r => r.GetByIdAsync(command.Id)).ReturnsAsync((Bean)null!);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.That(result.Status, Is.EqualTo(ResultStatus.Failure));
            Assert.That(result.ErrorMessage, Is.EqualTo("Bean not found."));
        }

        [Test]
        public async Task Handle_ShouldReturnError_WhenImageNotFound()
        {
            var command = CreateValidCommand();

            var bean = CreateValidBean();

            _beanRepositoryMock.Setup(r => r.GetByIdAsync(command.Id)).ReturnsAsync(bean);
            _imageRepositoryMock.Setup(r => r.GetByIdAsync(bean.ImageId)).ReturnsAsync((BeanImage)null!);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.That(result.Status, Is.EqualTo(ResultStatus.Error));
            Assert.That(result.ErrorMessage, Is.EqualTo("An unexpected error occurred while updating the bean."));
        }

        [Test]
        public async Task Handle_ShouldCreateCountry_IfNotFound()
        {
            var command = CreateValidCommand();

            var bean = CreateValidBean();

            var image = new BeanImage { Id = TestImageId, Url = "http://old.com" };
            var newCountry = new Country { Id = Guid.NewGuid(), Name = command.CountryName };

            _beanRepositoryMock.Setup(r => r.GetByIdAsync(command.Id)).ReturnsAsync(bean);
            _imageRepositoryMock.Setup(r => r.GetByIdAsync(bean.ImageId)).ReturnsAsync(image);
            _countryRepositoryMock.Setup(r => r.GetByNameAsync(command.CountryName)).ReturnsAsync((Country)null!);
            _countryRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<Country>())).ReturnsAsync(newCountry);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.That(result.Status, Is.EqualTo(ResultStatus.Success));
            Assert.That(result.Value, Is.EqualTo(bean.Id));
        }

        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenValid()
        {
            var command = CreateValidCommand();

            var bean = CreateValidBean();

            var image = new BeanImage { Id = TestImageId, Url = "http://old.com" };
            var country = new Country { Id = Guid.NewGuid(), Name = command.CountryName };

            _beanRepositoryMock.Setup(r => r.GetByIdAsync(command.Id)).ReturnsAsync(bean);
            _imageRepositoryMock.Setup(r => r.GetByIdAsync(bean.ImageId)).ReturnsAsync(image);
            _countryRepositoryMock.Setup(r => r.GetByNameAsync(command.CountryName)).ReturnsAsync(country);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.That(result.Status, Is.EqualTo(ResultStatus.Success));
            Assert.That(result.Value, Is.EqualTo(bean.Id));
        }

        [TestCase("", "Description", "Brown", "Country", "http://image.com", 9.99, "Name is required.")]
        [TestCase("Name", "", "Brown", "Country", "http://image.com", 9.99, "Description is required.")]
        [TestCase("Name", "Description", "", "Country", "http://image.com", 9.99, "Colour is required.")]
        [TestCase("Name", "Description", "Brown", "", "http://image.com", 9.99, "Country name is required.")]
        [TestCase("Name", "Description", "Brown", "Country", "", 9.99, "Image URL is required.")]
        [TestCase("Name", "Description", "Brown", "Country", "http://image.com", 0, "Cost must be greater than zero.")]
        public async Task Handle_ShouldReturnFailure_ForValidationErrors(
            string name, string description, string colour, string country, string imageUrl, decimal cost, string expectedError)
        {
            var command = new UpdateBeanCommand
            {
                Id = TestBeanId,
                Name = name,
                Description = description,
                Colour = colour,
                CountryName = country,
                ImageUrl = imageUrl,
                Cost = cost
            };

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.That(result.Status, Is.EqualTo(ResultStatus.Failure));
            Assert.That(result.ErrorMessage, Is.EqualTo(expectedError));
        }
    }
}
