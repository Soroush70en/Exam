using FluentValidation;
using Application.Features.Invoices.Queries;

namespace Application.Features.Invoices.Validator;

public class GetInvoiceByIdValidator : AbstractValidator<GetInvoiceByIdQry>
{
	public GetInvoiceByIdValidator()
	{
		RuleFor(x => x.PkInvoiceId)
			.NotNull()
			.NotEmpty().WithMessage("آیدی فاکتور نباید خالی باشد");
	}
}
