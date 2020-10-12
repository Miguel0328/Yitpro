using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.DTO
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string Token { get; set; }
    }

    public class UserDTOValidator : AbstractValidator<UserDTO>
    {
        public UserDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(5);
        }
    }
}
