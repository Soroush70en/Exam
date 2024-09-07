using MediatR;
using Domain.Entities;
using Application.Interfaces;

namespace Application.Features.Discounts.Queries;

public class GetDiscountByIdQryHandler : IRequestHandler<GetDiscountByIdQry, object>
{
    #region Variable's
    private readonly IUnitofWork _unw;
    private readonly IRepository<Discount> _rDiscount;
    #endregion

    #region Ctor's
    public GetDiscountByIdQryHandler(IUnitofWork unw)
    {
        _unw = unw;
        _rDiscount = _unw.Repository<Discount>();
    }
    #endregion

    #region Function's
    public async Task<object> Handle(GetDiscountByIdQry request, CancellationToken cancellationToken)
    {
        Discount Dist = await _rDiscount.GetFirstAsync(p => p.Status == 1 && p.PkId == request.PkDiscountId);

        if (Dist is null)
        {
            return "چنین رکوردی در دیتابیس وجود ندارد";
        }

        return new
        {
            Dist.PkId,
            Dist.Price,
            Dist.FkInvoiceId,
            Dist.FkInvoiceDetialId,
            Dist.DiscountType,
            Dist.DiscountTypeStr,
            Dist.CreateAt
        };
    }
    #endregion
}
