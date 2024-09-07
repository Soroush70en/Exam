using MediatR;

namespace Application.Features.Invoices.Commands;

public record ChangeStatusInvoiceCmd : IRequest<object>
{
    public Guid PkInvoiceId { get; set; }
}
