using IoC;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Invoices.Queries;
using Application.Features.Invoices.Commands;
using Application.Features.Invoices.Validator;
using FluentValidation;

namespace Exam.Controllers
{
    /// <summary>
    /// کنترلر مدیریت فاکتورها
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController(ISender _sender) : ControllerBase
    {
        #region Variable's
        private readonly ISender Sender = _sender;
        #endregion

        #region Action's
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByIdAsync(Guid? PkId)
        {
            GetInvoiceByIdQry Invoice = new()
            {
                PkInvoiceId = PkId
            };

            var Validate = new GetInvoiceByIdValidator().Validate(Invoice);

            if (!Validate.IsValid)
            {
                return Result.Ok(Validate.ToDictionary(), false);
            }

            var Res = await Sender.Send(Invoice);
            return Result.Ok(Res);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetAllInvoiceQry Qry)
        {
            GetAllInvoiceQry Invoice = new()
            {
                PkInvoiceId = Qry.PkInvoiceId,
                FkCustomerId = Qry.FkCustomerId,
                FkSellerId = Qry.FkSellerId,
                FkSellLineId = Qry.FkSellerId,
                Page = Qry.Page,
                PerPage = Qry.PerPage,
                InvStatus = Qry.InvStatus
            };

            var Res = await Sender.Send(Invoice);
            return Result.Ok(Res);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> InsertAsync(CreateInvoiceCmd Cmd)
        {
            CreateInvoiceCmd Invoice = new()
            {
                FkCustomerId = Cmd.FkCustomerId,
                FkSellerId = Cmd.FkSellerId,
                FkSellLineId = Cmd.FkSellLineId,
            };


            var Validate = new CreateInvoiceValidator().Validate(Invoice);

            if (!Validate.IsValid)
            {
                return Result.Ok(Validate.ToDictionary(), false);
            }

            var Res = await Sender.Send(Invoice);
            return Result.Ok(Res);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> ChangeStateAsync([FromQuery] Guid PkId)
        {
            ChangeStatusInvoiceCmd Invoice = new()
            {
                PkInvoiceId = PkId
            };

            var Res = await Sender.Send(Invoice);
            return Result.Ok(Res);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateAsync(UpdateInvoiceCmd Cmd)
        {
            UpdateInvoiceCmd Invoice = new()
            {
                FkCustomerId = Cmd.FkCustomerId,
                FkSellerId = Cmd.FkSellerId,
                FkSellLineId = Cmd.FkSellerId,
                PkInvoiceId = Cmd.PkInvoiceId
            };

            var Res = await Sender.Send(Invoice);
            return Result.Ok(Res);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> DeleteAsync([FromQuery] Guid PkId)
        {
            DeleteInvoiceCmd Invoice = new()
            {
                PkInvoiceId = PkId
            };

            var Res = await Sender.Send(Invoice);
            return Result.Ok(Res);
        }
        #endregion
    }
}
