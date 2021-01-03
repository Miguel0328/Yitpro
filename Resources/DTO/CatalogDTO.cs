using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resources.DTO
{
    public class CatalogDTO
    {
        public long Id { get; set; }
        public short CatalogId { get; set; }
        public string Description { get; set; }
        public string Alias { get; set; }
        public bool Active { get; set; }
        public bool Protected { get; set; }
    }

    public class DTOValidator : AbstractValidator<CatalogDTO>
    {
        public DTOValidator()
        {
            RuleFor(x => x.Alias).NotEmpty().MaximumLength(100).WithName("Alias");
            RuleFor(x => x.Description).NotEmpty().MaximumLength(500).WithName("Descripción");
        }
    }
}
