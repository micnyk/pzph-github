using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pzph.ServiceLayer.Bookings.Domain;
using Pzph.ServiceLayer.Common;

namespace Pzph.ServiceLayer.Bookings.Services
{
    public class BookingsService : IBookingsService
    {
        private readonly IBookingsRepository _bookingsRepository;

        public BookingsService(IBookingsRepository bookingsRepository)
        {
            _bookingsRepository = bookingsRepository;
        }

        public async Task<IEnumerable<Booking>> GetCustomerBookings(string customerId)
        {
            var bookings = await _bookingsRepository.GetCustomerBookings(customerId);
            return bookings;
        }
    }
}