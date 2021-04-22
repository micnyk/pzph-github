using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pzph.ServiceLayer.Bookings.Services;
using Pzph.ServiceLayer.Exceptions;
using Pzph.WebApplication.Extensions;
using Pzph.WebApplication.Models.Bookings;

namespace Pzph.WebApplication.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class BookingsManageController : Controller
    {
        private readonly IBookingsService _bookingsService;
        private readonly IMapper _mapper;

        public BookingsManageController(IBookingsService bookingsService, IMapper mapper)
        {
            _bookingsService = bookingsService;
            _mapper = mapper;
        }

        [HttpGet("/api/bookings")]
        public async Task<IActionResult> GetBookings()
        {
            try
            {
                var bookings = await _bookingsService.GetCustomerBookings(HttpContext.User.GetCustomerId());
                return Ok(_mapper.Map<IEnumerable<BookingModel>>(bookings));
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}