using MediatR;
using Domain.Enums;

namespace Application.Features.Discounts.Queries; 

public class GetAllDiscountsQry : IRequest<object>
{
    public DiscountType? DiscountType { get; set; }

    public Guid? FkInvoiceId { get; set; }

    public Guid? FkInvoiceDetialId { get; set; }

    public int? Page { get; set; } = 1;

    public int? PerPage { get; set; } = 25;
}
