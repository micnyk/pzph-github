using System;
using Pzph.ServiceLayer.Common;
using Pzph.ServiceLayer.Users.Domain;

namespace Pzph.ServiceLayer.Customers.Domain
{
    public class Customer : Entity
    {
        public Customer(User user)
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
            User = user;
            UserId = user.Id;
        }

        protected Customer()
        {
            // for Entity Framework needs
        }

        public virtual User User { get; }

        public string UserId { get; }
    }
}