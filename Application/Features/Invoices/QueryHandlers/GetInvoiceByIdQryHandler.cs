using MediatR;
using Domain.Entities;
using Application.Interfaces;
using Application.Features.Invoices.Queries;

namespace Application.Features.Invoices.QueryHandlers;

public class GetInvoiceByIdQryHandler : IRequestHandler<GetInvoiceByIdQry, object>
{
    #region Variable's
    private readonly IUnitofWork _unw;
    private readonly IRepository<Invoice> _rInv;
    #endregion

    #region Ctor's
    public GetInvoiceByIdQryHandler(IUnitofWork unw)
    {
        _unw = unw;
        _rInv = _unw.Repository<Invoice>();
    }
    #endregion


    #region Function's
    public async Task<object> Handle(GetInvoiceByIdQry request, CancellationToken cancellationToken)
    {
        Invoice Inv = await _rInv.GetFirstAsync(p => p.PkId == request.PkInvoiceId && p.Status == 1);

        if (Inv is null)
        {
            return "چنین فاکتوری وجود ندارد یا حذف شده است";
        }

        return new
        { 
            Invoice = new
            {
                Inv.PkId,
                Inv.InvStatusStr,
                Inv.Status,
                TotalCost = Inv.InvoiceDetails?.Sum(s => s.Cost * s.Count),
                TotalDiscount = Inv.Discounts?.Where(p => p.Status == 1).Sum(s => s.Price),
                Inv.CreateAt
            },
            Customer = new
            {
                Inv.Customer.PkId,
                Inv.Customer.Fullname
            },
            Seller = new
            {
                Inv.Seller.PkId,
                Inv.Seller.Fullname
            },
            Detail = Inv.InvoiceDetails?.Select(s => new
            {
                s.PkId,
                s.Product.Title,
                s.Count,
                s.Cost,
                Discount = s.Discounts?.Where(p => p.Status == 1).Sum(s => s.Price)
            })
        };
    }
    #endregion




}
