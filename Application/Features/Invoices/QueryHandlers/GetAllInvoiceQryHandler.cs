using MediatR;
using Domain.Entities;
using Application.Interfaces;
using System.Linq.Expressions;
using Application.Features.Invoices.Queries;

namespace Application.Features.Invoices.QueryHandlers;

public class GetAllInvoiceQryHandler : IRequestHandler<GetAllInvoiceQry, object>
{
    #region Variable's
    private readonly IUnitofWork _unw;
    private readonly IRepository<Invoice> _rInv;
    #endregion

    #region Ctor's
    public GetAllInvoiceQryHandler(IUnitofWork unw)
    {
        _unw = unw;
        _rInv = _unw.Repository<Invoice>();
    }
    #endregion


    #region Function's
    public async Task<object> Handle(GetAllInvoiceQry request, CancellationToken cancellationToken)
    {
        Expression<Func<Invoice, bool>> _exp = p => (!request.FkCustomerId.HasValue || p.FkCustomerId == request.FkCustomerId) &&
                                                    (!request.FkSellerId.HasValue || p.FkSellerId == request.FkSellerId) &&
                                                    (!request.FkSellLineId.HasValue || p.FkSellLineId == request.FkSellLineId) &&
                                                    (!request.InvStatus.HasValue || p.InvStatus == request.InvStatus) &&
                                                    (p.Status == 1);


        IEnumerable<Invoice> Invs = await _rInv.GetAsync(_exp, o => o.OrderByDescending(s => s.CreateAt) , request.Page, request.PerPage);

        return Invs.Select(Inv => new
        {
            Invoice = new
            {
                Inv.PkId,
                Inv.InvStatusStr,
                Inv.Status,
                TotalCost = Inv.InvoiceDetails?.Where(p => p.Status == 1).Sum(s => s.Cost * s.Count),
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
            Detail = Inv.InvoiceDetails?.Where(p => p.Status == 1).Select(s => new
            {
                s.PkId,
                s.Product.Title,
                s.Count,
                s.Cost,
                Discount = s.Discounts?.Where(p => p.Status == 1).Sum(s => s.Price)
            })
        });
    }
    #endregion
}
