using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pzph.ServiceLayer.Bookings.Domain;
using Pzph.ServiceLayer.Bookings.Services;

namespace Pzph.RepositoryLayer.Repositories
{
    public class BookingsRepository : IBookingsRepository
    {
        private readonly PzphDbContext _db;

        public BookingsRepository(PzphDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Booking>> GetCustomerBookings(string customerId)
        {
            return await _db.Bookings.Where(x => x.CustomerId == customerId).OrderByDescending(x => x.CreatedAt).ToListAsync();
        }
    }
}