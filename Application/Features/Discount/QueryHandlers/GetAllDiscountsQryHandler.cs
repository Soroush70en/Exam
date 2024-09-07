using MediatR;
using Domain.Entities;
using Application.Interfaces;
using Application.Features.Discounts.Commands;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Application.Features.Discounts.Queries;

public class GetAllDiscountsQryHandler : IRequestHandler<GetAllDiscountsQry, object>
{
    #region Variable's
    private readonly IUnitofWork _unw;
    private readonly IRepository<Discount> _rDiscount;
    #endregion

    #region Ctor's
    public GetAllDiscountsQryHandler(IUnitofWork unw)
    {
        _unw = unw;
        _rDiscount = _unw.Repository<Discount>();
    }
    #endregion

    #region Function's
    public async Task<object> Handle(GetAllDiscountsQry request, CancellationToken cancellationToken)
    {
        Expression<Func<Discount, bool>> _exp = p => (!request.FkInvoiceDetialId.HasValue || p.FkInvoiceDetialId == request.FkInvoiceDetialId) &&
                                                     (!request.FkInvoiceId.HasValue || p.FkInvoiceId == request.FkInvoiceId) &&
                                                     (!request.FkInvoiceDetialId.HasValue || p.FkInvoiceDetialId == request.FkInvoiceDetialId) &&
                                                     (!request.DiscountType.HasValue || p.DiscountType == request.DiscountType) &&
                                                     (p.Status == 1);

        IEnumerable<Discount> Dists = await _rDiscount.GetAsync(_exp, o => o.OrderByDescending(s => s.CreateAt), request.Page, request.PerPage);
        return Dists.Select(Dist => new
        {
            Dist.PkId,
            Dist.Price,
            Dist.FkInvoiceId,
            Dist.FkInvoiceDetialId,
            Dist.DiscountType,
            Dist.DiscountTypeStr,
            Dist.CreateAt
        });
    }
    #endregion
}
