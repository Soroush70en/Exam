using MediatR;
using Domain.Entities;
using Application.Interfaces;
using Application.Features.Discounts.Commands;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Discounts.CommandHandlers
{
    internal class UpdateDiscountCmdHandler : IRequestHandler<UpdateDiscountCmd, object>
    {
        #region Variable's
        private readonly IUnitofWork _unw;
        private readonly IRepository<Invoice> _rInv;
        private readonly IRepository<Discount> _rDiscount;
        #endregion

        #region Ctor's
        public UpdateDiscountCmdHandler(IUnitofWork unw)
        {
            _unw = unw;
            _rInv = _unw.Repository<Invoice>();
            _rDiscount = _unw.Repository<Discount>();
        }
        #endregion

        #region Function's
        public async Task<object> Handle(UpdateDiscountCmd request, CancellationToken cancellationToken)
        {
            Discount Dist = await _rDiscount.GetFirstAsync(p => p.PkId == request.PkDiscountId && p.Status == 1);

            if (Dist is null)
            {
                return "چنین رکوردی وجود ندارد یا حذف شده است";
            }

            Invoice Inv = await _rInv.GetFirstAsync(p => p.PkId == request.FkInvoiceId && p.Status == 1);

            if (Inv is null)
            {
                return "چنین فاکتوری وجود ندارد یا حذف شده است";
            }

            if (Inv.InvStatus == Domain.Enums.InvoiceStatus.Final)
            {
                return "به دلیل نهایی بودن وضعیت فاکتور امکان ویرایش تخفیف وجود ندارد";
            }

            if (request.DiscountType == Domain.Enums.DiscountType.Row)
            {
                if (request.FkInvoiceDetialId is null)
                {
                    return "در حالتی که نوع تخفیف ردیفی انتخاب شده باید حتما آیدی جزئیات فاکتور پر باشد";
                }
            }
            else
            {
                request.FkInvoiceDetialId = null;
            }

            long TotalCost = Inv.InvoiceDetails.Where(p => p.Status == 1).Sum(s => s.Cost);
            long TotalDiscount = await _unw.GetContext().Discounts.Where(p => p.FkInvoiceId == Inv.PkId && p.Status == 1).SumAsync(s => s.Price, cancellationToken: cancellationToken);

            if ((TotalCost - TotalDiscount - (request.Price - Dist.Price)) < 0)
            {
                return "به دلیل بیشتر شدن مبلغ تخفیف از مبلغ کل فاکتور امکان اضافه شدن این تخفیف وجود ندارد";
            }

            Dist.FkInvoiceId = request.FkInvoiceId ?? Dist.FkInvoiceId;
            Dist.FkInvoiceDetialId = request.FkInvoiceDetialId ?? Dist.FkInvoiceDetialId;
            Dist.Price = request.Price ?? Dist.Price;

            await _rDiscount.UpdateAsync(Dist);
            if (await _unw.SaveChangeAsync())
            {
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
            else
            {
                return "مشکل در ثبت داده در دیتابیس";
            }
        }
        #endregion
    }
}
