using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.DTO
{
    public class RoleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public bool Protected { get; set; }
    }

    public class RolePermisssionDTO
    {
        public int MenuId { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public byte Level { get; set; }
        public bool Access { get; set; }
        public bool Create { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
    }

    public class RoleDTOValidator : AbstractValidator<RoleDTO>
    {
        public RoleDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100).WithName("Nombre");
        }
    }
}
