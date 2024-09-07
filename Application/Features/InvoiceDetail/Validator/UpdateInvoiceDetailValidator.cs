using FluentValidation;
using Application.Features.InvoiceDetails.Commands;

namespace Application.Features.InvoiceDetails.Validator;

public class UpdateInvoiceDetailValidator : AbstractValidator<UpdateInvoiceDetailCmd>
{
    public UpdateInvoiceDetailValidator()
    {
        RuleFor(x => x.Cost)
             .NotNull().WithMessage("مبلغ نباید خالی باشد")
             .GreaterThan(0).WithMessage("مبلغ باید بزرگتر از صفر باشد");

        RuleFor(x => x.Count)
            .NotNull().WithMessage("تعداد نباید خالی باشد")
            .GreaterThan(0).WithMessage("تعداد باید بزرگتر از صفر باشد");
    }
}
