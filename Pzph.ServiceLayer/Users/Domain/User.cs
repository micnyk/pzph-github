using Microsoft.AspNetCore.Identity;
using Pzph.ServiceLayer.Contractors.Domain;
using Pzph.ServiceLayer.Customers.Domain;

namespace Pzph.ServiceLayer.Users.Domain
{
    public class User : IdentityUser
    {
        public User()
        {
            Contractor = new Contractor(this);
            Customer = new Customer(this);
        }

        public string FullName { get; set; }
        public virtual Contractor Contractor { get; private set; }
        public virtual Customer Customer { get; private set; }
    }
}