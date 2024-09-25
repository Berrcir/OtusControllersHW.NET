using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.DataAccess.Repositories;
using PromoCodeFactory.WebHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Сотрудники
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Role> _roleRepository;

        public EmployeesController(IRepository<Employee> employeeRepository, IRepository<Role> roleRepository)
        {
            _employeeRepository = employeeRepository;
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// Получить данные всех сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<EmployeeShortResponse>> GetEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();

            var employeesModelList = employees.Select(x =>
                new EmployeeShortResponse()
                {
                    Id = x.Id,
                    Email = x.Email,
                    FullName = x.FullName,
                }).ToList();

            return employeesModelList;
        }

        /// <summary>
        /// Получить данные сотрудника по Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            EmployeeResponse employeeModel = new EmployeeResponse(employee);

            return employeeModel;
        }

        /// <summary>
        /// Создать нового сотрудника
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<EmployeeCreationResponse>> CreateEmployeeAsync(NewEmployeeData employeeData)
        {
            IEnumerable<Role> allRoles = await _roleRepository.GetAllAsync();

            List<Role> employeeRoles = allRoles.Where(role => employeeData.Roles.Contains(role.Id)).ToList();

            if (!employeeRoles.Any())
            {
                return BadRequest();
            }

            Employee employee = new Employee(employeeData.FirstName,
                                             employeeData.LastName,
                                             employeeData.Email,
                                             employeeRoles);
            _employeeRepository.Add(employee);

            EmployeeCreationResponse employeeCreationModel = new EmployeeCreationResponse()
            {
                Id = employee.Id,
                FullName = employee.FullName,
            };

            return employeeCreationModel;
        }

        /// <summary>
        /// Обновить сотрудника сотрудника по Id
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<EmployeeResponse>> UpdateEmployeeByIdAsync(Guid id, NewEmployeeData employeeData)
        {
            Employee employee = await _employeeRepository.GetByIdAsync(id);

            if (employee is null)
            {
                return NotFound();
            }

            IEnumerable<Role> allRoles = await _roleRepository.GetAllAsync();

            List<Role> employeeRoles = allRoles.Where(role => employeeData.Roles.Contains(role.Id)).ToList();

            if (!employeeRoles.Any())
            {
                return BadRequest();
            }

            employee.FirstName = employeeData.FirstName;
            employee.LastName = employeeData.LastName;
            employee.Email = employeeData.Email;
            employee.Roles = employeeRoles;

            EmployeeResponse employeeModel = new EmployeeResponse(employee);

            return employeeModel;
        }

        /// <summary>
        /// Удалить сотрудника сотрудника по Id
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteEmployeeByIdAsync(Guid id)
        {
            Employee employee = await _employeeRepository.GetByIdAsync(id);

            if (employee is null)
            {
                return NotFound();
            }

            _employeeRepository.Remove(employee);

            return Ok();
        }
    }
}