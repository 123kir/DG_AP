using AirPort_PRO_NuGet_Logger.Contracts;
using AirPort_PRO_NuGet_Logger.Contracts.Models;
using DataGridAirPort.Storage.Memory;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace AirPort_PRO_NuGet_Logger.Storage.Memory.Tests
{
    /// <summary>
    /// Тесты для класса <see cref="MemoryAirPlaneStorage"/>
    /// </summary>
    public class PlaneStorageMemoryTests
    {
        private readonly IAirPortStorage airPortStorage;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PlaneStorageMemoryTests"/>
        /// </summary>
        public PlaneStorageMemoryTests()
        {
            airPortStorage = new MemoryAirPlaneStorage();
        }

        /// <summary>
        /// При пустом списке нет ошибок <see cref="IAirPortStorage.GetAllAsync"/>
        /// </summary>
        [Fact]
        public async Task GetAllShouldNotThrow()
        {
            // Act
            Func<Task> act = () => airPortStorage.GetAllAsync();

            // Assert
            await act.Should().NotThrowAsync();
        }


        /// <summary>
        /// Получает пустой список <see cref="IAirPortStorage.GetAllAsync"/>
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await airPortStorage.GetAllAsync();

            // Assert
            result.Should()
            .NotBeNull()
            .And.BeEmpty();
        }

        /// <summary>
        /// Тест: Метод <see cref="IAirPortStorage.AddAsync"/> должен вернуть добавленный объект.
        /// </summary>
        [Fact]
        public async Task AddShouldReturnValue()
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

            // Act
            var result = await airPortStorage.AddAsync(model);

            // Assert
            result.Should().NotBeNull()
                .And.BeEquivalentTo(new
                {
                    model.Id_Flight,
                    model.Type,
                });
        }

        /// <summary>
        /// Удаление из хранилища
        /// </summary>
        [Fact]
        public async Task DeleteShouldReturnTrue()
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

            // Act
            await airPortStorage.AddAsync(model);
            var result = await airPortStorage.DeleteAsync(model.Id_Flight);

            var nailList = await airPortStorage.GetAllAsync();
            var tryGetModel = nailList.FirstOrDefault(x => x.Id_Flight == model.Id_Flight);

            // Assert
            result.Should().BeTrue();
            tryGetModel.Should().BeNull();
        }

        /// <summary>
        /// Изменение данных в хранилище
        /// </summary>
        [Fact]
        public async Task EditShouldUpdateStorageData()
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

            // Act
            await airPortStorage.AddAsync(model);
            await airPortStorage.EditAsync(model);
            var applicantList = await airPortStorage.GetAllAsync();
            var result = applicantList.FirstOrDefault(x => x.Id_Flight == model.Id_Flight);

            // Assert
            result?.Should().NotBeNull();
            result?.Id_Flight.Should().Be(model.Id_Flight);
            result?.Type.Should().Be(Type_cs.Empty);
        }
    }
}
