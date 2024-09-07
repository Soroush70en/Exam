using MediatR;

namespace Application.Features.Invoices.Queries;

public class GetInvoiceByIdQry : IRequest<object>
{
    public Guid? PkInvoiceId { get; set; }
}
