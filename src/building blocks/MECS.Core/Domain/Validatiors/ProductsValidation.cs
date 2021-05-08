using FluentValidation;
using MECS.Core.Domain.Entities;

namespace MECS.Core.Domain.Validatiors
{
    public class ProductsValidation : AbstractValidator<Product>
    {
        public ProductsValidation()
        {
            const int NAME_MIN_LENGTH = 1;
            const int NAME_MAX_LENGTH = 250;
            const int DESCRIPTION_MIN_LENGTH = 1;
            const int DESCRIPTION_MAX_LENGTH = 500;
            const int IMAGE_MIN_LENGTH = 1;
            const int IMAGE_MAX_LENGTH = 250;


            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("O campo Name é obrigatório.")
                .Length(NAME_MIN_LENGTH, NAME_MAX_LENGTH)
                .WithMessage($"O Campo Name deve ter entre {NAME_MIN_LENGTH} e {NAME_MAX_LENGTH} caracteres.");

            RuleFor(x => x.Description)
                .NotNull()
                .WithMessage("O campo Name é obrigatório.")
                .Length(DESCRIPTION_MIN_LENGTH, DESCRIPTION_MAX_LENGTH)
                .WithMessage($"O Campo Name deve ter entre {DESCRIPTION_MIN_LENGTH} e {DESCRIPTION_MAX_LENGTH} caracteres.");

            RuleFor(x => x.Image)
                .NotNull()
                .WithMessage("O campo Name é obrigatório.")
                .Length(IMAGE_MIN_LENGTH, IMAGE_MAX_LENGTH)
                .WithMessage($"O Campo Name deve ter entre {IMAGE_MIN_LENGTH} e {IMAGE_MAX_LENGTH} caracteres.");
        }
    }
}
