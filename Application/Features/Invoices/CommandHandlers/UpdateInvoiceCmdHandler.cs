using MediatR;
using Domain.Entities;
using Application.Interfaces;
using Application.Features.Invoices.Commands;

namespace Application.Features.Invoices.CommandHandlers
{
    public class UpdateInvoiceCmdHandler : IRequestHandler<UpdateInvoiceCmd, object>
    {
        #region Variable's
        private readonly IUnitofWork _unw;
        private readonly IRepository<Invoice> _rInv;
        #endregion

        #region Ctor's
        public UpdateInvoiceCmdHandler(IUnitofWork unw)
        {
            _unw = unw;
            _rInv = _unw.Repository<Invoice>();
        }
        #endregion

        #region Function's
        public async Task<object> Handle(UpdateInvoiceCmd request, CancellationToken cancellationToken)
        {
            Invoice Inv = await _rInv.FindAsync(request.PkInvoiceId);

            if (Inv is null)
            {
                return "چنین فاکتور وجود ندارد";
            }

            if (Inv.InvStatus == Domain.Enums.InvoiceStatus.Final)
            {
                return "فاکتور در وضعیت نهایی است  هیچ گونه تعییری در آن مجاز نمی‌باشد";
            }

            if (Inv.FkSellLineId != request.FkSellLineId)
            {
                if (Inv.InvoiceDetails.Count > 0)
                {
                    return "به دلیل داشتن جزئیات فاکتور، تغییر لاین فروش مجاز نمی‌باشد";
                }
            }

            Inv.FkSellerId = request.FkSellerId ?? Inv.FkSellerId;
            Inv.FkSellLineId = request.FkSellLineId ?? Inv.FkSellLineId;
            Inv.FkCustomerId = request.FkCustomerId ?? Inv.FkCustomerId;

            await _rInv.UpdateAsync(Inv);
            if (await _unw.SaveChangeAsync())
            {
                return new[]
                {
                    _= new
                    {
                        Inv.PkId,
                        Inv.FkCustomerId,
                        Inv.FkSellerId,
                        Inv.InvStatus,
                        Inv.InvStatusStr,
                        Inv.CreateAt
                    }
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
