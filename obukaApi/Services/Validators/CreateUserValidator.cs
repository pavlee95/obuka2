using DTO.In;
using FluentValidation;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validators
{
    public class CreateUserValidator : AbstractValidator<UserDto>
    {
        public CreateUserValidator(BazaContext context)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .Must(n => !context.Users.Any(r => r.Email == n))
                .WithMessage("Email is not valid");
        }
    }
}
