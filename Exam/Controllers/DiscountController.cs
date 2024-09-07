using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Discounts.Commands;
using Application.Features.Discounts.Queries;
using IoC;
using Application.Features.Invoices.Validator;
using Application.Features.Discounts.Validator;
using FluentValidation;

namespace Exam.Controllers
{
    /// <summary>
    /// کنترلر مدیریت فاکتورها
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DiscountController(ISender _sender) : ControllerBase
    {
        #region Variable's
        private readonly ISender Sender = _sender;
        #endregion

        #region Action's
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByIdAsync(Guid PkId)
        {
            GetDiscountByIdQry Invoice = new()
            {
                PkDiscountId = PkId
            };

            var Res = await Sender.Send(Invoice);
            return Result.Ok(Res);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetAllDiscountsQry Qry)
        {
            GetAllDiscountsQry Invoice = new()
            {
                DiscountType = Qry.DiscountType,
                FkInvoiceDetialId = Qry.FkInvoiceDetialId,
                FkInvoiceId = Qry.FkInvoiceId
            };

            var Res = await Sender.Send(Invoice);
            return Result.Ok(Res);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> InsertAsync(CreateDiscountCmd Cmd)
        {
            CreateDiscountCmd Invoice = new()
            {
                DiscountType = Cmd.DiscountType,
                FkInvoiceDetialId = Cmd.FkInvoiceDetialId,
                FkInvoiceId = Cmd.FkInvoiceId,
                Price = Cmd.Price,
            };

            var Validate = new CreateDiscountValidator().Validate(Invoice);

            if (!Validate.IsValid)
            {
                return Result.Ok(Validate.ToDictionary(), false);
            }

            var Res = await Sender.Send(Invoice);
            return Result.Ok(Res);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateAsync(UpdateDiscountCmd Cmd)
        {
            UpdateDiscountCmd Invoice = new()
            {
                PkDiscountId = Cmd.PkDiscountId,
                DiscountType = Cmd.DiscountType,
                FkInvoiceDetialId = Cmd.FkInvoiceDetialId,
                FkInvoiceId = Cmd.FkInvoiceId,
                Price = Cmd.Price
            };

            var Validate = new UpdateDiscountValidator().Validate(Invoice);

            if (!Validate.IsValid)
            {
                return Result.Ok(Validate.ToDictionary(), false);
            }

            var Res = await Sender.Send(Invoice);
            return Result.Ok(Res);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> DeleteAsync([FromQuery] Guid PkId)
        {
            DeleteDiscountCmd Invoice = new()
            {
                PkDiscountId = PkId
            };

            var Res = await Sender.Send(Invoice);
            return Result.Ok(Res);
        }
        #endregion
    }
}
