using AirPort_PRO_NuGet_Logger.Contracts.Models;
using System.Data.Entity;

namespace AirPort_PRO_NuGet_Logger.DB
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public class DataGridContext : DbContext
    {
        /// <summary>
        /// Конструктор контекста базы данных
        /// </summary>
        public DataGridContext() : base("DataGridPlans")
        {
        }

        /// <summary>
        /// Таблица <see cref="Plane"/> в базе данных
        /// </summary>
        public DbSet<Plane> Plane { get; set; }
    }
}
