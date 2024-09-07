using MediatR;
using Domain.Entities;
using Application.Interfaces;
using Application.Features.InvoiceDetails.Commands;

namespace Application.Features.InvoiceDetails.CommandHandlers;

public class UpdateInvoiceDetailCmdHandler : IRequestHandler<UpdateInvoiceDetailCmd, object>
{
    #region Variable's
    private readonly IUnitofWork _unw;
    private readonly IRepository<InvoiceDetail> _rInvDetail;
    #endregion

    #region Ctor's
    public UpdateInvoiceDetailCmdHandler(IUnitofWork unw)
    {
        _unw = unw;
        _rInvDetail = _unw.Repository<InvoiceDetail>();
    }
    #endregion


    #region Function's
    public async Task<object> Handle(UpdateInvoiceDetailCmd request, CancellationToken cancellationToken)
    {
        InvoiceDetail InvDetail = await _rInvDetail.GetFirstAsync(p => p.PkId == request.PkInvoiceDetailId && p.Status == 1);

        if (InvDetail is null)
        {
            return "چنین رکوردی در فاکتور وجود ندارد و یا حذف شده است";
        }

        if (InvDetail.Invoice.InvStatus == Domain.Enums.InvoiceStatus.Final)
        {
            return "به دلیل نهایی بودن وضعیت فاکتور امکان ویرایش رکورد جزئیات در فاکتور وجود ندارد";
        }

        // To Do Validation


        InvDetail.Cost = request.Cost ?? InvDetail.Cost;
        InvDetail.Count = request.Count ?? InvDetail.Count;
        InvDetail.FkProductId = request.FkProductId ?? InvDetail.FkProductId;

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
