using System;

namespace PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    public class Preference
        : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}