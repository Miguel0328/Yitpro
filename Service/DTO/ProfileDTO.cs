using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.DTO
{
    public class ProfileDTO
    {
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Token { get; set; }
        public List<MenuDTO> Menus { get; set; }
    }

    public class ProfileDTOValidator : AbstractValidator<ProfileDTO>
    {
        public ProfileDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(5);
        }
    }
}
