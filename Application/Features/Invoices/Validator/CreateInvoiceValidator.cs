using FluentValidation;
using Application.Features.Invoices.Commands;

namespace Application.Features.Invoices.Validator;

public class CreateInvoiceValidator : AbstractValidator<CreateInvoiceCmd>
{
	public CreateInvoiceValidator()
	{
		RuleFor(x => x.FkCustomerId)
			.NotNull().WithMessage("آیدی مشتری نباید خالی باشد")
            .NotEmpty().WithMessage("آیدی مشتری نباید خالی باشد");

        RuleFor(x => x.FkSellerId)
            .NotNull().WithMessage("آیدی مشتری نباید خالی باشد")
            .NotEmpty().WithMessage("آیدی فروشنده نباید خالی باشد");

        RuleFor(x => x.FkSellLineId)
            .NotNull().WithMessage("آیدی مشتری نباید خالی باشد")
            .NotEmpty().WithMessage("آیدی لاین فروش نباید خالی باشد");
    }
}
