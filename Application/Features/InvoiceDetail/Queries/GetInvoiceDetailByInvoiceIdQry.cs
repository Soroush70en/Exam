using MediatR;

namespace Application.Features.InvoiceDetails.Queries;

public class GetInvoiceDetailByInvoiceIdQry : IRequest<object>
{
    public Guid FkInvoiceId { get; set; }
}
