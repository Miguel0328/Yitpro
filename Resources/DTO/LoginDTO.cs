using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resources.DTO
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
