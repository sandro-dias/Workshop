using Application.Data;
using Application.Data.Specification;
using Application.UseCases.Service.GetReport;
using Application.UseCases.Service.GetReport.Input;
using AutoFixture;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Application.UseCases.Service.GetReport
{
    public class GetReportUseCaseTest
    {
        private readonly Fixture _fixture = new();
        private readonly GetReportUseCase _useCase;
        private readonly Mock<IValidator<GetReportInput>> _validator = new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<ILogger<GetReportUseCase>> _logger = new();

        public GetReportUseCaseTest()
        {
            _useCase = new GetReportUseCase(_validator.Object, _unitOfWork.Object, _logger.Object);
        }

        [Fact]
        public async Task ShouldGetReportSuccessfully()
        {
            //Arrange
            var fakeInput = _fixture.Create<GetReportInput>();
            var fakeValidationResult = new ValidationResult();
            _validator.Setup(x => x.Validate(It.IsAny<GetReportInput>())).Returns(fakeValidationResult);
            _unitOfWork.Setup(x => x.ServiceRepository.ListAsync(It.IsAny<GetServicesReportByDatePeriodSpecification>(), It.IsAny<CancellationToken>())).ReturnsAsync(_fixture.CreateMany<Domain.Entities.Service>(1));

            //Act
            var result = await _useCase.ExecuteAsync(fakeInput);

            //Assert
            result.Should().NotBeNull();
        }
    }
}
