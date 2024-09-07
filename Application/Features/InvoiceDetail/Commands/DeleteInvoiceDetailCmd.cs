using MediatR;

namespace Application.Features.InvoiceDetails.Commands;

public record DeleteInvoiceDetailCmd : IRequest<object>
{
    public Guid PkInvoiceDetailId { get; set; }
}
