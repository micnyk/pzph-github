using Pzph.ServiceLayer.Common;
using Pzph.ServiceLayer.Contractors.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pzph.ServiceLayer.Categories.Domain
{
    public class Category : Entity
    {
        public string Name { get; }
        public virtual ICollection<Service> Services { get; } = new List<Service>();
    }
}
