using AirPort_PRO_NuGet_Logger.Contracts.Models;

namespace AirPort_PRO_NuGet_Logger.AirPortManager.Models
{
    /// <summary>
    /// Модель для представления статистики по самолетам
    /// </summary>
    public class PlaneStatsModel : IPlaneStats
    {
        /// <summary>
        /// Общее количество самолетов
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Общее количество пассажиров
        /// </summary>
        public int Total_passengers { get; set; }

        /// <summary>
        /// Общее количество членов экипажа на всех самолетах
        /// </summary>
        public int Entire_crew { get; set; }

        /// <summary>
        /// Общее количество сборов от всех пассажиров
        /// </summary>
        public decimal Total_coins { get; set; }
    }
}
