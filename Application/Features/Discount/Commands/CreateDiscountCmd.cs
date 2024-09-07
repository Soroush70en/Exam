using MediatR;
using Domain.Enums;

namespace Application.Features.Discounts.Commands;

public record CreateDiscountCmd : IRequest<object>
{
    public long? Price { get; set; }

    public DiscountType? DiscountType { get; set; }

    public Guid? FkInvoiceId { get; set; }

    public Guid? FkInvoiceDetialId { get; set; }
}
