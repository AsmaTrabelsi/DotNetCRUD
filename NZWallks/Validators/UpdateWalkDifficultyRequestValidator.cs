using FluentValidation;
using NZWallks.Models.DTO;

namespace NZWallks.Validators
{
    public class UpdateWalkDifficultyRequestValidator :AbstractValidator<UpdateWalkDifficulty>
    {
        public UpdateWalkDifficultyRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
