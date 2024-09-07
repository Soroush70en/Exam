using MediatR;
using Domain.Entities;
using Application.Interfaces;
using Application.Features.InvoiceDetails.Queries;

namespace Application.Features.InvoiceDetails.QueryHandlers
{
    public class GetInvoiceDetailByIdQryHandler : IRequestHandler<GetInvoiceDetailByIdQry, object>
    {
        #region Variable's
        private readonly IUnitofWork _unw;
        private readonly IRepository<InvoiceDetail> _rInvDetail;
        #endregion

        #region Ctor's
        public GetInvoiceDetailByIdQryHandler(IUnitofWork unw)
        {
            _unw = unw;
            _rInvDetail = _unw.Repository<InvoiceDetail>();
        }
        #endregion


        #region Function's
        public async Task<object> Handle(GetInvoiceDetailByIdQry request, CancellationToken cancellationToken)
        {
            InvoiceDetail InvDetail = await _rInvDetail.GetFirstAsync(p => p.PkId == request.PkInvoiceDetaiId && p.Status == 1);

            if (InvDetail is null)
            {
                return "چنین کوردی وجود ندارد یا حذف شده است";
            }

            return new
            {
                InvDetail.PkId,
                InvDetail.FkProductId,
                InvDetail.Product.Title,
                InvDetail.Cost,
                Discount = InvDetail.Discounts?.Where(p => p.Status == 1).Sum(s => s.Price),
                InvDetail.Count,
                InvDetail.CreateAt,
                Invoice = new
                {
                    PkId = InvDetail.FkInvoiceId,

                    Seller = new
                    {
                        PkId = InvDetail.Invoice.FkSellerId,
                        InvDetail.Invoice.Seller.Fullname,
                    },

                    Customer = new
                    {
                        PkId = InvDetail.Invoice.FkCustomerId,
                        InvDetail.Invoice.Customer.Fullname,
                    }
                }
            };
        }
        #endregion
    }
}
