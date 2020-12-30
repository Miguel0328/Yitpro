using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resources.DTO
{
    public class ClientDTO
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public byte ProjectCount { get; set; }
        public bool Active { get; set; }
    }

    public class ClientDTOValidator : AbstractValidator<ClientDTO>
    {
        public ClientDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(300).WithName("Nombre");
        }
    }
}
