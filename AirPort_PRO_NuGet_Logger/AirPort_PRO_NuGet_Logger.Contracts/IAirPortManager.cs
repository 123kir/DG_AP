using AirPort_PRO_NuGet_Logger.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirPort_PRO_NuGet_Logger.Contracts
{
    /// <summary>
    /// Интерфейс IAirPort определяет контракт для работы с аэропортом и его самолетами
    /// </summary>
    public interface IAirPort
    {
        /// <summary>
        /// Асинхронное получение всех данных
        /// </summary
        Task<IReadOnlyCollection<Plane>> GetAllAsync();
        /// <summary>
        /// Асинхронная операция добавления
        /// </summary>
        Task<Plane> AddAsync(Plane plane);
        /// <summary>
        /// Асинхронная операция изменения
        /// </summary>
        Task EditAsync(Plane plane);
        /// <summary>
        /// Асинхронная операция удаления
        /// </summary>
        Task<bool> DeleteAsync(Guid id);
        /// <summary>
        /// Асинхронная получение суммарных данных
        /// </summary>
        Task<IPlaneStats> GetStatsAsync();
    }
}
