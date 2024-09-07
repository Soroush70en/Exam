using MediatR;
using Domain.Entities;
using Application.Interfaces;
using Application.Features.Invoices.Commands;


namespace Application.Features.Invoices.CommandHandlers
{
    public class DeleteInvoiceCmdHandler : IRequestHandler<DeleteInvoiceCmd, object>
    {
        #region Variable's
        private readonly IUnitofWork _unw;
        private readonly IRepository<Invoice> _rInv;
        #endregion

        #region Ctor's
        public DeleteInvoiceCmdHandler(IUnitofWork unw)
        {
            _unw = unw;
            _rInv = _unw.Repository<Invoice>();
        }
        #endregion

        #region Function's
        public async Task<object> Handle(DeleteInvoiceCmd request, CancellationToken cancellationToken)
        {
            Invoice Inv = await _rInv.FindAsync(request.PkInvoiceId);

            if (Inv is null)
            {
                return "چنین فاکتور وجود ندارد";
            }

            if (Inv.InvStatus == Domain.Enums.InvoiceStatus.Final)
            {
                return "به دلیل نهایی بودن فاکتور اجازه حذف آن وجود ندارد";
            }

            Inv.Status = 0;

            await _rInv.UpdateAsync(Inv);
            if (await _unw.SaveChangeAsync())
            {
                return Inv;
            }
            else
            {
                return "مشکل در ثبت داده در دیتابیس";
            }
        }
        #endregion
    }
}
