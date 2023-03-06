using FluentValidation;
using NZWallks.Models.DTO;

namespace NZWallks.Validators
{
    public class AddWalkDifficultyRequestValidator : AbstractValidator<AddWalkDifficultyRequest>
    {
        public AddWalkDifficultyRequestValidator()
        {
            RuleFor(x => x.code).NotEmpty();

        }

    }
}
