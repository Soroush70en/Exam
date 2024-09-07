using FluentValidation;
using Application.Features.Discounts.Commands;

namespace Application.Features.Discounts.Validator
{
    public class UpdateDiscountValidator : AbstractValidator<UpdateDiscountCmd>
    {
        public UpdateDiscountValidator()
        {
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("مبلغ تخفیف باید بیشتر از  صفر باشد");
        }
    }
}
