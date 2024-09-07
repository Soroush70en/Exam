using MediatR;
using Domain.Entities;
using Application.Interfaces;
using Application.Features.Invoices.Commands;
using Microsoft.EntityFrameworkCore;


namespace Application.Features.Invoices.CommandHandlers
{
    public class ChangeStatusInvoiceCmdHandler : IRequestHandler<ChangeStatusInvoiceCmd, object>
    {
        #region Variable's
        private readonly IUnitofWork _unw;
        private readonly IRepository<Invoice> _rInv;
        #endregion

        #region Ctor's
        public ChangeStatusInvoiceCmdHandler(IUnitofWork unw)
        {
            _unw = unw;
            _rInv = _unw.Repository<Invoice>();
        }
        #endregion

        #region Function's
        public async Task<object> Handle(ChangeStatusInvoiceCmd request, CancellationToken cancellationToken)
        {
            Invoice Inv = await _rInv.FindAsync(request.PkInvoiceId);

            if (Inv is null)
            {
                return "چنین فاکتور وجود ندارد";
            }

            if (Inv.InvStatus == Domain.Enums.InvoiceStatus.Final)
            {
                return " در حال حاضر در حالت نهایی قرار دارد";
            }

            if (Inv.InvoiceDetails.Count == 0)
            {
                return " فاکتور جاری هیج جزئیاتی ندارد";
            }

            long TotalCost = await _unw.GetContext().InvoiceDetails.Where(p => p.Invoice.InvStatus == Domain.Enums.InvoiceStatus.Final && p.Invoice.FkCustomerId == Inv.FkCustomerId).SumAsync(s => s.Cost * s.Count, cancellationToken: cancellationToken);

            long TotalDiscount = await _unw.GetContext().Discounts.Where(p => p.Invoice.InvStatus == Domain.Enums.InvoiceStatus.Final && p.Invoice.FkCustomerId == Inv.FkCustomerId).SumAsync(s => (long)s.Price, cancellationToken: cancellationToken);

            if ((TotalCost - TotalDiscount) > 10_000_000)
            {
                return "به دلیل داشتن بدهی اجازه نهایی کردن این فاکتور وجود ندارد";
            }

            Inv.InvStatus = Domain.Enums.InvoiceStatus.Final;

            await _rInv.UpdateAsync(Inv);
            if (await _unw.SaveChangeAsync())
            {
                return new
                {
                    _= new
                    {
                        Inv.PkId,
                        Inv.FkCustomerId,
                        Inv.FkSellerId,
                        Inv.InvStatus,
                        Inv.InvStatusStr,
                        Inv.CreateAt,
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
