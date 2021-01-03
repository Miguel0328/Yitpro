using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resources.DTO
{
    public class UserDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Photo { get; set; }
        public string Department { get; set; }
        public bool Active { get; set; }
    }

    public class UserFilterDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public short? RoleId { get; set; }
        public bool? Active { get; set; }
    }

    public class UserDetailDTO
    {
        public long Id { get; set; }
        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public string Email { get; set; }
        public DateTime AdmissionDate { get; set; }
        public short RoleId { get; set; }
        public long? ManagerId { get; set; }
        public IFormFile Photo { get; set; }
        public string PhotoUrl { get; set; }
        public long DepartmentId { get; set; }
        public bool Capture { get; set; }
        public bool Active { get; set; }
        public bool Locked { get; set; }
    }

    public class UserPermisssionDTO
    {
        public int MenuId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public byte Level { get; set; }
        public bool Access { get; set; }
        public bool Create { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
    }

    public class UserDetailDTOValidator: AbstractValidator<UserDetailDTO>
    {
        public UserDetailDTOValidator()
        {
            RuleFor(x => x.EmployeeNumber).NotEmpty().MaximumLength(50).WithName("Numero de empleado");
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100).WithName("Nombre");
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(100).WithName("Apellido paterno");
            RuleFor(x => x.SecondLastName).NotEmpty().MaximumLength(100).WithName("Apellido materno");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(200).WithName("Email");
            RuleFor(x => x.AdmissionDate).NotEmpty().WithName("Fecha de ingreso");
            RuleFor(x => x.RoleId).NotEmpty().WithName("Rol");
            RuleFor(x => x.DepartmentId).NotEmpty().WithName("Departamento");
        }
    }
}
