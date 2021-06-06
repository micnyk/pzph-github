using System;
using Pzph.ServiceLayer.Categories.Domain;
using Pzph.ServiceLayer.Common;

namespace Pzph.ServiceLayer.Contractors.Domain
{
    public class Service : Entity
    {
        public Service(Contractor contractor, string name, string description)
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
            Contractor = contractor;
            Name = name;
            Description = description;
        }

        protected Service()
        {
        }

        public virtual Contractor Contractor { get; }
        public virtual Category Category { get; }

        public string Name { get; }

        public string Description { get; }
        
    }
}