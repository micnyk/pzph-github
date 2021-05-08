using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pzph.WebApplication.Models.Services
{
    public class ServiceModel : BaseModel
    {
        public string Name { get; set; }
        public string ContractorId { get; set; }
        public string Description { get; set; }
    }
}
