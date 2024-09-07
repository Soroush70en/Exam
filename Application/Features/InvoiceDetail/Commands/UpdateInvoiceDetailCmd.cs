using MediatR;

namespace Application.Features.InvoiceDetails.Commands;

public record UpdateInvoiceDetailCmd : IRequest<object>
{
    public Guid PkInvoiceDetailId { get; set; }

    public Guid? FkProductId { get; set; }

    public Guid? FkInvoiceId { get; set; }

    public long? Cost { get; set; }

    public int? Count { get; set; }
}
