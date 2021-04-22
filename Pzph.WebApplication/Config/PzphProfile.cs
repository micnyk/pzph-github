using AutoMapper;
using Pzph.ServiceLayer.Bookings.Domain;
using Pzph.WebApplication.Models.Bookings;

namespace Pzph.WebApplication.Config
{
    public class PzphProfile : Profile
    {
        public PzphProfile()
        {
            CreateMap<Booking, BookingModel>();
        }
    }
}