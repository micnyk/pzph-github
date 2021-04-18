using System;
using System.Collections.Generic;
using Pzph.ServiceLayer.Common;
using Pzph.ServiceLayer.Users.Domain;

namespace Pzph.ServiceLayer.Contractors.Domain
{
    public class Contractor : Entity
    {
        public Contractor(User user)
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
            User = user;
            UserId = user.Id;
        }

        protected Contractor()
        {
            // for Entity Framework needs
        }

        public virtual User User { get; }

        public string UserId { get; }
        public virtual ICollection<Service> OfferedServices { get; } = new List<Service>();
    }
}