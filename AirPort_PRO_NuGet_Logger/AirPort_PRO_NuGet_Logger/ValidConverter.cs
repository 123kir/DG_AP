using AirPort_PRO_NuGet_Logger.Contracts.Models;

namespace AirPort_PRO_NuGet_Logger
{
    /// <summary>
    /// Методы приведения одного типа к другому
    /// </summary>
    public static class ValidConverter
    {
        /// <summary>
        /// Привести <see cref="Plane"/> к <see cref="ValidPlane"/>
        /// </summary>
        public static Plane ToValidPlane(ValidPlane validPlane)
        {
            return new Plane()
            {
                Id_Flight = validPlane.Id_Flight,
                Type = validPlane.Type,
                Number_Flight = validPlane.Number_Flight,
                Number_passenger = validPlane.Number_passenger,
                Passenger_fee = validPlane.Passenger_fee,
                Number_crew = validPlane.Number_crew,
                Crew_fee = validPlane.Crew_fee,
                Present_ = validPlane.Present_,
                DTPArrival = validPlane.DTPArrival,
                RentalAmount = validPlane.RentalAmount,
            };
        }

        /// <summary>
        /// Привести <see cref="ValidPlane"/> к <see cref="Plane"/>
        /// </summary>
        public static ValidPlane ToPlane(Plane plane)
        {
            return new ValidPlane()
            {
                Id_Flight = plane.Id_Flight,
                Type = plane.Type,
                Number_Flight = plane.Number_Flight,
                Number_passenger = plane.Number_passenger,
                Passenger_fee = plane.Passenger_fee,
                Number_crew = plane.Number_crew,
                Crew_fee = plane.Crew_fee,
                Present_ = plane.Present_,
                DTPArrival = plane.DTPArrival,
                RentalAmount = plane.RentalAmount,
            };
        }
    }
}
