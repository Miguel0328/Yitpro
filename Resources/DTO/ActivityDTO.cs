using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resources.DTO
{
    public class ActivityDTO
    {
        public long Id { get; set; }
        public string Activity { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string Phase { get; set; }
        public UserDTO Assigned { get; set; }
        public UserDTO Responsible { get; set; }
        public int AssignedTime { get; set; }
        public int FinalTime { get; set; }
        public bool Urgent { get; set; }
        public bool Critical { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? FinalDate { get; set; }
    }

    public class ActivityDetailDTO
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public long AssignedId { get; set; }
        public long ResponsibleId { get; set; }
        public long PhaseId { get; set; }
        public long ClasificationId { get; set; }
        public string AssignedTime { get; set; }
        public string EstimatedTime { get; set; }
        public string FinalTime { get; set; }
        public List<DateTime> Period { get; set; }
        public string Requirement { get; set; }
        public string Description { get; set; }
        public bool Critical { get; set; }
        public bool Planned { get; set; }
        public bool Urgent { get; set; }
        public bool Status { get; set; }
    }

    public class ActivityDetailDTOValidator : AbstractValidator<ActivityDetailDTO>
    {
        public ActivityDetailDTOValidator()
        {
            RuleFor(x => x.Requirement).NotEmpty().MaximumLength(500).WithName("Requerimiento");
            RuleFor(x => x.Description).NotEmpty().MaximumLength(4000).WithName("Descripción");
            RuleFor(x => x.Period).NotEmpty().WithName("Periodo");
            RuleFor(x => x.AssignedTime).NotEmpty().MinimumLength(5).MaximumLength(5).WithName("Horas asignadas");
            RuleFor(x => x.EstimatedTime).NotEmpty().MinimumLength(5).MaximumLength(5).WithName("Horas estimadas");
            RuleFor(x => x.ProjectId).NotEmpty().WithName("Cliente");
            RuleFor(x => x.PhaseId).NotEmpty().WithName("Líder");
            RuleFor(x => x.ClasificationId).NotEmpty().WithName("Metodología");
            RuleFor(x => x.AssignedId).NotEmpty().WithName("Estatus");
            RuleFor(x => x.ResponsibleId).NotEmpty().WithName("Tipo");
        }
    }
}
