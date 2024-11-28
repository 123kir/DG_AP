using System;
using System.ComponentModel.DataAnnotations;

namespace AirPort_PRO_NuGet_Logger.Contracts.Models
{
    /// <summary>
    /// Класс, представляющий информацию о самолете
    /// </summary>
    public class ValidPlane
    {
        /// <summary>
        /// Уникальный идентификатор рейса
        /// </summary>
        public Guid Id_Flight { get; set; }

        /// <summary>
        ///  Модель 
        /// </summary>

        /// <intheridoc cref="Models.Type_cs"/>
        public Type_cs Type { get; set; }

        /// <summary>
        /// Номер рейса 
        /// </summary>
        [Required]
        [StringLength(11, MinimumLength = 1)]
        public string Number_Flight { get; set; }

        /// <summary>
        /// Кол-во пассажиров 
        /// </summary>
        [Required]
        [Range(0, 450)]
        public int Number_passenger { get; set; }

        /// <summary>
        /// Сбор на пассажиров 
        /// </summary>
        [Required]
        [Range(0, 1000000)]
        public decimal Passenger_fee { get; set; }

        /// <summary>
        /// Кол-во экипажа 
        /// </summary>
        [Required]
        [Range(0, 100)]
        public int Number_crew { get; set; }

        /// <summary>
        /// Сборы на экипаж 
        /// </summary>
        [Required]
        [Range(0, 100000)]
        public decimal Crew_fee { get; set; }

        /// <summary>
        /// Надбавка 
        /// </summary>
        [Required]
        [Range(0, 100)]
        public decimal Present_ { get; set; }

        /// <summary>
        /// Дата прилета
        /// </summary>
        public DateTime DTPArrival { get; set; }

        /// <summary>
        /// Выручка
        /// </summary>
        public decimal RentalAmount { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Plane"/>.
        /// </summary>
        /// <remarks>
        /// Конструктор устанавливает значение <see cref="DTPArrival"/> на текущее время, 
        /// а <see cref="RentalAmount"/> инициализируется нулем.
        /// </remarks>
        public ValidPlane()
        {
            DTPArrival = DateTime.Now;
            RentalAmount = 0;
        }

        /// <summary>
        /// Вычисляет общую стоимость рейса
        /// </summary>
        public decimal CalculateTotalCost()
        {
            return (Number_passenger * Passenger_fee + Number_crew * Crew_fee) * Present_;
        }

    }
}
