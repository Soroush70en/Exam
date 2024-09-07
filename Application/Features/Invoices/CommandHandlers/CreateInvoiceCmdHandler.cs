using MediatR;
using Domain.Entities;
using Application.Interfaces;
using Application.Features.Discounts.Commands;
using Application.Features.Invoices.Commands;

namespace Application.Features.Invoices.CommandHandlers
{
    public class CreateInvoiceCmdHandler : IRequestHandler<CreateInvoiceCmd, object>
    {
        #region Variable's
        private readonly IUnitofWork _unw;
        private readonly IRepository<Invoice> _rInv;
        private readonly IRepository<SellLineSeller> _rLineSeller;
        #endregion

        #region Ctor's
        public CreateInvoiceCmdHandler(IUnitofWork unw)
        {
            _unw = unw;
            _rInv = _unw.Repository<Invoice>();
            _rLineSeller = _unw.Repository<SellLineSeller>();
        }
        #endregion

        #region Function's
        public async Task<object> Handle(CreateInvoiceCmd request, CancellationToken cancellationToken)
        {
            SellLineSeller LineSeller = await _rLineSeller.GetFirstAsync(p => p.FkSellerId == request.FkSellerId && p.FkSellLineId == request.FkSellLineId);

            if (LineSeller is null)
            {
                return "برای این فروشنده و لاین فروش رکورد معادلی وجود ندارد";
            }

            Invoice Inv = new()
            {
                PkId = Guid.NewGuid(),
                FkCustomerId = request.FkCustomerId.Value,
                FkSellLineId = request.FkSellLineId.Value,
                FkSellerId = request.FkSellerId.Value,
            };

            await _rInv.InsertAsync(Inv);

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
