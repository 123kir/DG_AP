namespace AirPort_PRO_NuGet_Logger.Contracts.Models
{
    /// <summary>
    /// Интерфейс для статистики по самолетам в аэропорту
    /// </summary>
    public interface IPlaneStats
    {
        /// <summary>
        /// Общее количество самолетов
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Общее количество пассажиров
        /// </summary>
        int Total_passengers { get; }

        /// <summary>
        /// Общее количество членов экипажа на всех самолетах
        /// </summary>
        int Entire_crew { get; }

        /// <summary>
        /// Общее количество сборов от всех пассажиров
        /// </summary>
        decimal Total_coins { get; }
    }
}
