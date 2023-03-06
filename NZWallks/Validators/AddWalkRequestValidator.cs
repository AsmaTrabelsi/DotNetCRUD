using FluentValidation;
using NZWallks.Models.DTO;

namespace NZWallks.Validators
{
    public class AddWalkRequestValidator :AbstractValidator<AddWalkRequest>
    {
        public AddWalkRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0);
        }
    }
}
