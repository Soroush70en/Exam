using MediatR;
using Domain.Entities;
using Application.Interfaces;
using Application.Features.InvoiceDetails.Commands;

namespace Application.Features.InvoiceDetails.CommandHandlers;

public class DeleteInvoiceDetailCmdHandler : IRequestHandler<DeleteInvoiceDetailCmd, object>
{
    #region Variable's
    private readonly IUnitofWork _unw;
    private readonly IRepository<InvoiceDetail> _rInvDetail;
    #endregion

    #region Ctor's
    public DeleteInvoiceDetailCmdHandler(IUnitofWork unw)
    {
        _unw = unw;
        _rInvDetail = _unw.Repository<InvoiceDetail>();
    }
    #endregion


    #region Function's
    public async Task<object> Handle(DeleteInvoiceDetailCmd request, CancellationToken cancellationToken)
    {
        InvoiceDetail InvDetail = await _rInvDetail.FindAsync(request.PkInvoiceDetailId);

        if (InvDetail is null)
        {
            return "چنین رکوردی در فاکتور وجود ندارد";
        }

        if (InvDetail.Invoice.InvStatus == Domain.Enums.InvoiceStatus.Final)
        {
            return "به دلیل نهایی بودن وضعیت فاکتور امکان حذف رکورد جزئیات در فاکتور وجود ندارد";
        }

        InvDetail.Status = 0;

        await _rInvDetail.UpdateAsync(InvDetail);
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
