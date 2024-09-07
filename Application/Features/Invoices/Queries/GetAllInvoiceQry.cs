using Domain.Enums;
using MediatR;

namespace Application.Features.Invoices.Queries;

public class GetAllInvoiceQry : IRequest<object>
{
    public Guid? PkInvoiceId { get; set; }

    public Guid? FkSellLineId { get; set; }

    public Guid? FkSellerId { get; set; }

    public Guid? FkCustomerId { get; set; }

    public InvoiceStatus? InvStatus { get; set; }

    public int? Page { get; set; } = 1;

    public int? PerPage { get; set; } = 25;

}
