using System;

namespace PromoCodeFactory.WebHost.Models
{
    public class NewEmployeeData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid[] Roles { get; set; }

    }
}