using System.ComponentModel;

namespace AirPort_PRO_NuGet_Logger.Contracts.Models
{
    /// <summary>
    /// Перечисление для обозначения типов самолетов
    /// </summary>
    public enum Type_cs
    {
        /// <summary>
        /// Пусто
        /// </summary>
        [Description("Пусто")]
        Empty = 1,

        /// <summary>
        /// Эйрбас
        /// </summary>
        [Description("Эйрбас")]
        Airbus = 2,

        /// <summary>
        /// ОАК
        /// </summary>
        [Description("ОАК")]
        UAC = 3,

        /// <summary>
        /// Боинг
        /// </summary>
        [Description("Боинг")]
        Boeing = 4,
    }
}
