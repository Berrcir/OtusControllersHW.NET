using System;
using System.Collections.Generic;
using System.Linq;
using PromoCodeFactory.Core.Domain.Administration;

namespace PromoCodeFactory.WebHost.Models
{
    public class EmployeeResponse
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public List<RoleItemResponse> Roles { get; set; }

        public int AppliedPromocodesCount { get; set; }

        public EmployeeResponse() { }
        public EmployeeResponse(Employee employee)
        {
            Id = employee.Id;
            FullName = employee.FullName;
            Email = employee.Email;
            Roles = employee.Roles.Select(x => new RoleItemResponse()
            {
                Name = x.Name,
                Description = x.Description
            }).ToList();
            AppliedPromocodesCount = employee.AppliedPromocodesCount;
        }
    }
}