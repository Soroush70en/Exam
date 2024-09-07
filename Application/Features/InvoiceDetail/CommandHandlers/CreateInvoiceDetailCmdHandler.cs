using MediatR;
using Domain.Entities;
using Application.Interfaces;
using Application.Features.InvoiceDetails.Commands;

namespace Application.Features.InvoiceDetails.CommandHandlers;

public class CreateInvoiceDetailCmdHandler : IRequestHandler<CreateInvoiceDetailCmd, object>
{
    #region Variable's
    private readonly IUnitofWork _unw;
    private readonly IRepository<Invoice> _rInv;
    private readonly IRepository<InvoiceDetail> _rInvDetail;
    private readonly IRepository<SellLineProduct> _rLineProduct;
    #endregion

    #region Ctor's
    public CreateInvoiceDetailCmdHandler(IUnitofWork unw)
    {
        _unw = unw;
        _rInv = _unw.Repository<Invoice>();
        _rInvDetail = _unw.Repository<InvoiceDetail>();
        _rLineProduct = _unw.Repository<SellLineProduct>();
    }
    #endregion


    #region Function's
    public async Task<object> Handle(CreateInvoiceDetailCmd request, CancellationToken cancellationToken)
    {
        Invoice Inv = await _rInv.FindAsync(request.FkInvoiceId);

        if (Inv.InvoiceDetails.Select(s => s.FkProductId).Contains(request.FkProductId.Value))
        {
            return "این محصول از قبل در لیست جزئیات فاکتور درج شده است";
        }

        if (Inv.InvStatus == Domain.Enums.InvoiceStatus.Final)
        {
            return "فاکتور در وضعیت نهایی است  هیچ گونه تعییری در آن مجاز نمی‌باشد";
        }

        IEnumerable<SellLineProduct> LineProduct = await _rLineProduct.GetAsync(p => p.FkSellLineId == Inv.FkSellLineId && p.FkProductId == request.FkProductId);

        if (!LineProduct.Any())
        {
            return "محصول انتخاب شده در لیست محصولات موجود در این لاین فروش قرار ندارد";
        }

        InvoiceDetail InvDetail = new()
        {
            Cost = request.Cost.Value,
            Count = request.Count.Value,
            PkId = Guid.NewGuid(),
            FkInvoiceId = request.FkInvoiceId.Value,
            FkProductId = request.FkProductId.Value
        };

        await _rInvDetail.InsertAsync(InvDetail);
        if (await _unw.SaveChangeAsync())
        {
            return new
            {
                InvDetail.PkId,
                InvDetail.FkInvoiceId,
                InvDetail.FkProductId,
                InvDetail.Cost,
                InvDetail.Count
            };
        }
        else
        {
            return "مشکل در ثبت داده در دیتابیس";
        }
    }
    #endregion
}
