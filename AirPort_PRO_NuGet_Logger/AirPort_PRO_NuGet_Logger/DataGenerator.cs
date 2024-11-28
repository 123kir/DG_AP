using AirPort_PRO_NuGet_Logger.Contracts.Models;
using System;


namespace DataGridAirPort
{
    internal class DataGenerator
    {
        /// <summary>
        /// Генерация уникального идентификатора для рейса
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static Plane CreatePlane(Action<Plane> settings = null)
        {
            var result = new Plane
            {
                Id_Flight = Guid.NewGuid(),
            };

            settings?.Invoke(result);
            return result;
        }
    }
}
