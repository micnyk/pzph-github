using System;

namespace Pzph.ServiceLayer.Common
{
    public class Entity
    {
        public string Id { get; protected set; }

        public DateTime CreatedAt { get; protected set; }
    }
}