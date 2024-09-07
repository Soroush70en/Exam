using MediatR;

namespace Application.Features.InvoiceDetails.Queries;

public class GetInvoiceDetailByIdQry : IRequest<object>
{
    public Guid PkInvoiceDetaiId { get; set; }
}
