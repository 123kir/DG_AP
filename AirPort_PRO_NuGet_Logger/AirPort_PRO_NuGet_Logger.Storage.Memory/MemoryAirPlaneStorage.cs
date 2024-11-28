using AirPort_PRO_NuGet_Logger.Contracts;
using AirPort_PRO_NuGet_Logger.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGridAirPort.Storage.Memory
{
    /// <summary>
    ///  Класс MemoryAirPlaneStorage реализует интерфейс IAirPortStorage для хранения информации о самолетах в памяти
    /// </summary>
    public class MemoryAirPlaneStorage : IAirPortStorage
    {
        private List<Plane> airplane;

        /// <summary>
        /// Конструктор инициализирует пустой список самолетов
        /// </summary>
        public MemoryAirPlaneStorage()
        {
            airplane = new List<Plane>();
        }
        /// <summary>
        /// Метод для добавления нового самолета в хранилище
        /// </summary>
        /// <param name="plane"></param>
        /// <returns></returns>
        public Task<Plane> AddAsync(Plane plane)
        {
            airplane.Add(plane);
            return Task.FromResult(plane);
        }
        /// <summary>
        /// Метод для удаления самолета по его идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Возвращаем false, если самолет не найден</returns>
        public Task<bool> DeleteAsync(Guid id)
        {
            var plane = airplane.FirstOrDefault(x => x.Id_Flight == id);

            if (plane != null)
            {
                airplane.Remove(plane);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        /// <summary>
        /// Метод для редактирования существующего самолета
        /// </summary>
        /// <param name="plane"></param>
        /// <returns></returns>
        public Task EditAsync(Plane plane)
        {

            var target = airplane.FirstOrDefault(x => x.Id_Flight == plane.Id_Flight);

            if (plane != null)
            {

                target.Type = plane.Type;
                target.Number_Flight = plane.Number_Flight;
                target.Number_passenger = plane.Number_passenger;
                target.Passenger_fee = plane.Passenger_fee;
                target.Number_crew = plane.Number_crew;
                target.Crew_fee = plane.Crew_fee;
                target.Present_ = plane.Present_;
                target.DTPArrival = plane.DTPArrival;
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Метод для получения всех самолетов
        /// </summary>
        /// <returns></returns>
        public Task<IReadOnlyCollection<Plane>> GetAllAsync()
            => Task.FromResult<IReadOnlyCollection<Plane>>(airplane);
    }
}
