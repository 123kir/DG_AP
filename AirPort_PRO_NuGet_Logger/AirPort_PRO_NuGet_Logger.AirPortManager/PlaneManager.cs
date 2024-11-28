using AirPort_PRO_NuGet_Logger.Contracts;
using AirPort_PRO_NuGet_Logger.Contracts.Models;
using AirPort_PRO_NuGet_Logger.AirPortManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Diagnostics;



namespace AirPort_PRO_NuGet_Logger.AirPortManager
{
    /// <summary>
    /// Менеджер для работы с самолетами в аэропорту
    /// </summary>
    public class PlaneManager_cs : IAirPort
    {
        private readonly IAirPortStorage airPortStorage;
        private readonly ILogger logger;
        private const string InfoLoggerTxt = "Действие {@applicant} c id {ID}, выполненно за {Milliseconds} мс";
        private const string ErrorLoggerTxt = "Действие {@applicant} c id {ID}, не было выполненно";

        /// <summary>
        /// Конструктор для инициализации менеджера самолетов
        /// </summary>
        /// <param name="airPortStorage">Хранилище данных аэропорта</param> // 
        public PlaneManager_cs(IAirPortStorage airPortStorage, ILogger logger)
        {
            this.logger = logger;
            this.airPortStorage = airPortStorage;
        }

        /// <summary>
        /// Асинхронно добавляет новый самолет в хранилище
        /// </summary>
        /// <param name="plane">Модель самолета для добавления</param> 
        /// <returns>Возвращает добавленный самолет</returns> 
        public async Task<Plane> AddAsync(Plane plane)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Plane result;
            try
            {
                result = await airPortStorage.AddAsync(plane);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                LoggingHelper.LogErrorPlane(
                    logger,
                    nameof(IAirPort.AddAsync),
                    plane.Id_Flight,
                    stopwatch.ElapsedMilliseconds,
                    ex.Message,
                    plane.Number_Flight
                    );
                return null;
            }
            stopwatch.Stop();
            LoggingHelper.LogInfoAirPlane(
                logger,
                nameof(IAirPort.AddAsync),
                plane.Id_Flight,
                stopwatch.ElapsedMilliseconds,
                plane.Number_Flight
                );
            return result;
        }

        /// <summary>
        /// Асинхронно удаляет самолет по указанному идентификатору
        /// </summary>
        /// <param name="id">Идентификатор самолета, который нужно удалить</param>
        /// <returns>Возвращает результат удаления</returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            bool result;

            try
            {
                result = await airPortStorage.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                LoggingHelper.LogErrorPlane(logger, nameof(IAirPort.DeleteAsync),
                    id,
                    stopwatch.ElapsedMilliseconds,
                    ex.Message
                    );
                return false;
            }
            stopwatch.Stop();
            LoggingHelper.LogInfoAirPlane(logger, nameof(IAirPort.DeleteAsync),
                id,
                stopwatch.ElapsedMilliseconds
                );
            return result;
        }

        /// <summary>
        /// Асинхронно редактирует информацию о самолете
        /// </summary>
        /// <param name="plane">Модель самолета с обновленной информацией</param>
        /// <returns>Возвращает задачу, представляющую асинхронную операцию</returns>
        public async Task EditAsync(Plane plane)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                await airPortStorage.EditAsync(plane);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                LoggingHelper.LogErrorPlane(logger, nameof(IAirPort.EditAsync),
                    plane.Id_Flight,
                    stopwatch.ElapsedMilliseconds,
                    ex.Message,
                    plane.Number_Flight
                    );
            }
            stopwatch.Stop();
            LoggingHelper.LogInfoAirPlane(logger, nameof(IAirPort.EditAsync),
                plane.Id_Flight,
                stopwatch.ElapsedMilliseconds,
                plane.Number_Flight
                );
        }

        /// <summary>
        /// Асинхронно получает информацию о всех самолетах
        /// </summary>
        /// <returns>Возвращает коллекцию всех самолетов</returns>
        public async Task<IReadOnlyCollection<Plane>> GetAllAsync()
        {
            try
            {
                return await airPortStorage.GetAllAsync();
            }
            catch (Exception ex)
            {
                LoggingHelper.LogError(logger, nameof(IAirPort.GetAllAsync), ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Асинхронно получает статистику по самолетам
        /// </summary>
        /// <returns>Возвращает объект статистики по самолетам</returns>
        public async Task<IPlaneStats> GetStatsAsync()
        {
            var items = await airPortStorage.GetAllAsync();

            foreach (var plane in items)
            {
                plane.RentalAmount = (decimal)plane.CalculateTotalCost();
            }

            return new PlaneStatsModel
            {
                Count = items.Count,
                Total_passengers = items.Sum(x => x.Number_passenger),

                Entire_crew = items.Sum(x => x.Number_crew),
                Total_coins = items.Sum(x => x.RentalAmount),
            };
        }
    }
}
