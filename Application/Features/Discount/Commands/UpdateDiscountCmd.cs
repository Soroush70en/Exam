using MediatR;
using Domain.Enums;

namespace Application.Features.Discounts.Commands;

public record UpdateDiscountCmd : IRequest<object>
{
    public Guid PkDiscountId { get; set; }

    public long? Price { get; set; }

    public DiscountType? DiscountType { get; set; }

    public Guid? FkInvoiceId { get; set; }

    public Guid? FkInvoiceDetialId { get; set; }
}
