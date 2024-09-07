using MediatR;
using Domain.Enums;

namespace Application.Features.Invoices.Commands;

public record UpdateInvoiceCmd : IRequest<object>
{
    public Guid PkInvoiceId { get; set; }

    public Guid? FkSellLineId { get; set; }

    public Guid? FkSellerId { get; set; }

    public Guid? FkCustomerId { get; set; }
}
