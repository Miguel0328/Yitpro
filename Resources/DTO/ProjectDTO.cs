using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resources.DTO
{
    public class ProjectDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Client { get; set; }
        public string Leader { get; set; }
        public string LeaderPhoto { get; set; }
        public bool Active { get; set; }
    }

    public class ProjectFilterDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public short? ClientId { get; set; }
        public bool? Active { get; set; }
    }

    public class ProjectDetailDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public short ClientId { get; set; }
        public long LeaderId { get; set; }
        public bool Active { get; set; }
    }

    public class ProjectTeamDTO
    {
        public short Id { get; set; }
        public string User { get; set; }
        public string UserPhoto { get; set; }
    }

    public class ProjectDetailDTOValidator : AbstractValidator<ProjectDetailDTO>
    {
        public ProjectDetailDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200).WithName("Nombre");
            RuleFor(x => x.Code).NotEmpty().MaximumLength(20).WithName("Clave");
            RuleFor(x => x.Description).NotEmpty().MaximumLength(1200).WithName("Descripción");
            RuleFor(x => x.ClientId).NotEmpty().WithName("Cliente");
            RuleFor(x => x.LeaderId).NotEmpty().WithName("Líder");
        }
    }
}
