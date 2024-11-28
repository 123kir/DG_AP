using AirPort_PRO_NuGet_Logger.Contracts;
using AirPort_PRO_NuGet_Logger.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AirPort_PRO_NuGet_Logger.DB
{
    /// <summary>
    /// Этот класс реализует интерфейс IAirPortStorage с использованием базы данных.
    /// </summary>
    public class DataBasePlaneStorage : IAirPortStorage
    {

        /// <summary>
        /// Добавляет новый рейс в базу данных.
        /// </summary>
        public async Task<Plane> AddAsync(Plane plane)
        {
            using (var context = new DataGridContext())
            {
                var raes = new Plane
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
                context.Planes.Add(plane);
                await context.SaveChangesAsync();
            }

            return plane;
        }

        /// <summary>
        /// Удаляет рейс из базы данных по идентификатору.
        /// </summary>
        public async Task<bool> DeleteAsync(Guid id_Flight)
        {
            using (var context = new DataGridContext())
            {
                var item = await context.Planes.FirstOrDefaultAsync(x => x.Id_Flight == id_Flight);
                if (item != null)
                {
                    context.Planes.Remove(item);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Обновляет существующий рейс в базе данных.
        /// </summary>
        public async Task EditAsync(Plane plane)
        {
            using (var context = new DataGridContext())
            {
                var target = await context.Planes.FirstOrDefaultAsync(x => x.Id_Flight == plane.Id_Flight);
                if (target != null)
                {
                    target.Type = plane.Type;
                    target.Number_Flight = plane.Number_Flight;
                    target.Number_passenger = plane.Number_passenger;
                    target.Passenger_fee = plane.Passenger_fee;
                    target.Number_crew = plane.Number_crew;
                    target.Crew_fee = plane.Crew_fee;
                    target.Present_ = plane.Present_;
                    target.DTPArrival = plane.DTPArrival;
                    target.RentalAmount = plane.RentalAmount;
                }
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Получает все рейсы из базы данных.
        /// </summary>
        public async Task<IReadOnlyCollection<Plane>> GetAllAsync()
        {
            using (var context = new DataGridContext())
            {
                var items = await context.Plane
                    .OrderByDescending(x => x.Type)
                    .ToListAsync()
                    ;
                return items;
            }
        }
    }
}

