using AirPort_PRO_NuGet_Logger.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirPort_PRO_NuGet_Logger.Contracts
{
    /// <summary>
    /// Интерфейс IAirPortStorage определяет контракт для хранилища самолетов
    /// </summary>
    public interface IAirPortStorage
    {
        /// <summary>
        /// Асинхронная получение суммарных данных
        /// </summary>
        Task<IReadOnlyCollection<Plane>> GetAllAsync();
        /// <summary>
        /// Асинхронная операция добавления
        /// </summary>
        Task<Plane> AddAsync(Plane Plane);
        /// <summary>
        /// Асинхронная операция изменения
        /// </summary>
        Task EditAsync(Plane Plane);
        /// <summary>
        /// Асинхронная операция удаления
        /// </summary>
        Task<bool> DeleteAsync(Guid Id);
    }
}
