using MediatR;
using Domain.Entities;
using Application.Interfaces;
using Application.Features.Discounts.Commands;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Discounts.CommandHandlers
{
    internal class CreateDiscountCmdHandler : IRequestHandler<CreateDiscountCmd, object>
    {
        #region Variable's
        private readonly IUnitofWork _unw;
        private readonly IRepository<Invoice> _rInv;
        private readonly IRepository<Discount> _rDiscount;
        #endregion

        #region Ctor's
        public CreateDiscountCmdHandler(IUnitofWork unw)
        {
            _unw = unw;
            _rInv = _unw.Repository<Invoice>();
            _rDiscount = _unw.Repository<Discount>();
        }
        #endregion

        #region Function's
        public async Task<object> Handle(CreateDiscountCmd request, CancellationToken cancellationToken)
        {
            Invoice Inv = await _rInv.GetFirstAsync(p => p.PkId == request.FkInvoiceId && p.Status == 1);

            if (Inv.InvStatus == Domain.Enums.InvoiceStatus.Final)
            {
                return "به دلیل نهایی بودن وضعیت فاکتور امکان اضافه شدن تخفیف وجود ندارد";
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

            if (!Inv.InvoiceDetails.Where(p => p.Status == 1 && p.PkId == request.FkInvoiceDetialId).Any())
            {
                return "ایدی انتخاب شده برای جزئیات فاکتور با آیدی انتخاب شده برای فاکتور مطابقت ندارد";
            }

            long TotalCost = Inv.InvoiceDetails.Where(p => p.Status == 1).Sum(s => s.Cost);
            long TotalDiscount = await _unw.GetContext().Discounts.Where(p => p.FkInvoiceId == Inv.PkId && p.Status == 1).SumAsync(s => s.Price, cancellationToken: cancellationToken);

            if ((TotalCost - TotalDiscount - request.Price) < 0)
            {
                return "به دلیل بیشتر شدن مبلغ تخفیف از مبلغ کل فاکتور امکان اضافه شدن این تخفیف وجود ندارد";
            }

            Discount Dist = new()
            {
                DiscountType = request.DiscountType.Value,
                Price = request.Price.Value,
                FkInvoiceId = request.FkInvoiceId.Value,
                FkInvoiceDetialId = request.FkInvoiceDetialId,
            };

            await _rDiscount.InsertAsync(Dist);
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
