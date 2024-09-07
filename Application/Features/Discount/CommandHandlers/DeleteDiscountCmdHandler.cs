using MediatR;
using Domain.Entities;
using Application.Interfaces;
using Application.Features.Discounts.Commands;

namespace Application.Features.Discounts.CommandHandlers
{
    internal class DeleteDiscountCmdHandler : IRequestHandler<DeleteDiscountCmd, object>
    {
        #region Variable's
        private readonly IUnitofWork _unw;
        private readonly IRepository<Invoice> _rInv;
        private readonly IRepository<Discount> _rDiscount;
        #endregion

        #region Ctor's
        public DeleteDiscountCmdHandler(IUnitofWork unw)
        {
            _unw = unw;
            _rInv = _unw.Repository<Invoice>();
            _rDiscount = _unw.Repository<Discount>();
        }
        #endregion

        #region Function's
        public async Task<object> Handle(DeleteDiscountCmd request, CancellationToken cancellationToken)
        {
            Discount Dist = await _rDiscount.GetFirstAsync(p => p.PkId == request.PkDiscountId && p.Status == 1);

            if (Dist is null)
            {
                return "چنین رکوردی وجود ندارد یا حذف شده است";
            }

            Invoice Inv = await _rInv.GetFirstAsync(p => p.PkId == Dist.FkInvoiceId && p.Status == 1);

            if (Inv is null)
            {
                return "چنین فاکتوری وجود ندارد یا حذف شده است";
            }

            if (Inv.InvStatus == Domain.Enums.InvoiceStatus.Final)
            {
                return "به دلیل نهایی بودن وضعیت فاکتور امکان حذف تخفیف وجود ندارد";
            }

            Dist.Status = 0;

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
