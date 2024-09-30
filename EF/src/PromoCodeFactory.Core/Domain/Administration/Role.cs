using PromoCodeFactory.Core.Domain;
using System;

namespace PromoCodeFactory.Core.Domain.Administration
{
    public class Role
        : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}