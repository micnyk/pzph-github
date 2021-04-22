using System.Collections.Generic;
using System.Threading.Tasks;
using Pzph.ServiceLayer.Bookings.Domain;

namespace Pzph.ServiceLayer.Bookings.Services
{
    public interface IBookingsRepository
    {
        Task<IEnumerable<Booking>> GetCustomerBookings(string customerId);
    }
}