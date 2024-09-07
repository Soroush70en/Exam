using MediatR;

namespace Application.Features.Invoices.Commands;

public record DeleteInvoiceCmd : IRequest<object>
{
    public Guid PkInvoiceId { get; set; }
}
