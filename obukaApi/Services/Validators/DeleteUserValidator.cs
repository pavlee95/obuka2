using FluentValidation;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validators
{
    public class DeleteUserValidator : AbstractValidator<DeleteDto>
    {
        public DeleteUserValidator(BazaContext context)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .Must(n => context.Users.Any(r => r.Id == n))
                .WithMessage("Id is not valid");
        }
    }
}
