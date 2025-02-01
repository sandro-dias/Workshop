using Application.Data;
using Application.Data.Specification;
using Application.Services.CreateWorkingDay.Input;
using AutoFixture;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Services.CreateWorkingDay;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Infrastructure.Services.CreateWorkingDay
{
    public class CreateWorkingDayServiceTest
    {
        private readonly Fixture _fixture = new();
        private CreateWorkingDayService _service;
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<ILogger<CreateWorkingDayService>> _logger = new();

        public CreateWorkingDayServiceTest()
        {
            _service = new CreateWorkingDayService(_unitOfWork.Object, _logger.Object);       
        }

        //TODO: REVISAR UNIT TEST
        [Fact]
        public async Task ShouldCreateWorkingDaySuccessfully()
        {
            //Arrange
            var fakeInput = _fixture.Build<CreateWorkingDayInput>().With(x => x.Date, new DateTime(2024, 04, 02)).Create();
            _unitOfWork.Setup(x => x.WorkingDayRepository.FirstOrDefaultAsync(It.IsAny<GetWorkingDayByWorkshopSpecification>(), It.IsAny<CancellationToken>()));
            _unitOfWork.Setup(x => x.WorkingDayRepository.AddAsync(It.IsAny<WorkingDay>(), It.IsAny<CancellationToken>())).ReturnsAsync(_fixture.Create<WorkingDay>());

            //Act
            var result = await _service.CreateWorkingDay(fakeInput);

            //Assert
            result.Should().NotBeNull();
            _unitOfWork.Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ShouldReturnWorkingDayAlreadyExists()
        {
            //Arrange
            var fakeInput = _fixture.Create<CreateWorkingDayInput>();
            _unitOfWork.Setup(x => x.WorkingDayRepository.FirstOrDefaultAsync(It.IsAny<GetWorkingDayByWorkshopSpecification>(), It.IsAny<CancellationToken>())).ReturnsAsync(_fixture.Create<WorkingDay>());

            //Act
            var result = await _service.CreateWorkingDay(fakeInput);

            //Assert
            result.Should().NotBeNull();
            _unitOfWork.Verify(x => x.WorkingDayRepository.FirstOrDefaultAsync(It.IsAny<GetWorkingDayByWorkshopSpecification>(), It.IsAny<CancellationToken>()), Times.Once);
            _unitOfWork.Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
