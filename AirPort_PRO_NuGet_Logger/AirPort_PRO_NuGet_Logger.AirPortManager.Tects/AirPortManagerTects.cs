using AirPort_PRO_NuGet_Logger.Contracts;
using AirPort_PRO_NuGet_Logger.Contracts.Models;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AirPort_PRO_NuGet_Logger.AirPortManager.Tects
{
    /// <summary>
    /// Тесты для класса <see cref="PlaneManager_cs"/>.
    /// </summary>
    public class PlaneManager_csTest
    {
        private readonly IAirPort airPortManager;
        private readonly Mock<IAirPortStorage> airPortStorageMock;
        private readonly Mock<ILogger> loggerMock;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PlaneManager_csTest"/>.
        /// </summary>
        public PlaneManager_csTest()
        {
            airPortStorageMock = new Mock<IAirPortStorage>();
            loggerMock = new Mock<ILogger>();

            loggerMock.Setup(x => x.Log(LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                null,
                It.IsAny<Func<It.IsAnyType, Exception, string>>())); 

            airPortManager = new PlaneManager_cs(airPortStorageMock.Object, loggerMock.Object);
        }

        /// <summary>
        /// Добавление в хранилище.
        /// Тест: метод <see cref="PlaneManager_cs.AddAsync"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var model = new Plane
            {
                Id_Flight = Guid.NewGuid(),
                Type = Type_cs.Empty,
                Number_Flight = "1Ы",
                Number_passenger = 1,
                Passenger_fee = 1,
                Number_crew = 1,
                Crew_fee = 1,
                Present_ = 1,
                DTPArrival = DateTime.Now,
            };
            airPortStorageMock.Setup(x => x.AddAsync(It.IsAny<Plane>()))
                .ReturnsAsync(model);

            // Act
            var result = await airPortManager.AddAsync(model);

            // Asset
            result.Should().NotBeNull()
                .And.Be(model);

            loggerMock.Verify(x => x.Log
            (LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((state, t) => state.ToString().Contains(nameof(IAirPort.AddAsync))),
            null,
            It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
            loggerMock.VerifyNoOtherCalls();

            airPortStorageMock.Verify(x => x.AddAsync(It.Is<Plane>(y => y.Id_Flight == model.Id_Flight)),
                Times.Once);
            airPortStorageMock.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Изменение данных в хранилище
        /// Тест: метод <see cref="PlaneManager_cs.AddAsync"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var model = new Plane
            {
                Id_Flight = Guid.NewGuid(),
                Type = Type_cs.Empty,
                Number_Flight = "1Ы",
                Number_passenger = 1,
                Passenger_fee = 1,
                Number_crew = 1,
                Crew_fee = 1,
                Present_ = 1,
                DTPArrival = DateTime.Now,
            };
            airPortStorageMock.Setup(x => x.EditAsync(It.IsAny<Plane>())).Returns(Task.CompletedTask);

            // Act
            await airPortManager.EditAsync(model);

            // Asset
            loggerMock.Verify(x => x.Log
            (LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((state, t) => state.ToString().Contains(nameof(IAirPort.EditAsync))),
            null,
            It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
            loggerMock.VerifyNoOtherCalls();

            airPortStorageMock.Verify(x => x.EditAsync(It.Is<Plane>(y => y.Id_Flight == model.Id_Flight)),
                Times.Once);
            airPortStorageMock.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Удаление данных в хранилище
        /// Тест: метод <see cref="PlaneManager_cs.AddAsync"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var model = new Plane
            {
                Id_Flight = Guid.NewGuid(),
                Type = Type_cs.Empty,
                Number_Flight = "1Ы",
                Number_passenger = 1,
                Passenger_fee = 1,
                Number_crew = 1,
                Crew_fee = 1,
                Present_ = 1,
                DTPArrival = DateTime.Now,
            };
            airPortStorageMock.Setup(x => x.DeleteAsync(model.Id_Flight))
                .ReturnsAsync(true);

            // Act
            var result = await airPortManager.DeleteAsync(model.Id_Flight);

            // Asset
            result.Should().BeTrue();

            loggerMock.Verify(x => x.Log
            (LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((state, t) => state.ToString().Contains(nameof(IAirPort.DeleteAsync))),
            null,
            It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
            loggerMock.VerifyNoOtherCalls();

            airPortStorageMock.Verify(x => x.DeleteAsync(model.Id_Flight),
                Times.Once);
            airPortStorageMock.VerifyNoOtherCalls();

        }
    }
}
