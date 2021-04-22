using System;
using Pzph.ServiceLayer.Common;
using Pzph.ServiceLayer.Customers.Domain;

namespace Pzph.ServiceLayer.Bookings.Domain
{
    public class Booking : Entity
    {
        public Booking(Customer customer)
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
            Customer = customer;
            CustomerId = customer.Id;
        }

        protected Booking()
        {
        }

        public virtual Customer Customer { get; }
        public string CustomerId { get; }
    }
}