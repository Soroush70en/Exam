using MediatR;

namespace Application.Features.Invoices.Commands;

public record CreateInvoiceCmd : IRequest<object>
{
    public Guid? FkSellLineId { get; set; }

    public Guid? FkSellerId { get; set; }

    public Guid? FkCustomerId { get; set; }
}
