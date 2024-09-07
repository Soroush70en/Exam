using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Features.InvoiceDetails.Queries;
using Application.Features.InvoiceDetails.Commands;
using IoC;
using Application.Features.Invoices.Validator;
using Application.Features.InvoiceDetails.Validator;

namespace Exam.Controllers
{
    /// <summary>
    /// کنترلر مدیریت فاکتورها
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class InvoiceDetailController(ISender _sender) : ControllerBase
    {
        #region Variable's
        private readonly ISender Sender = _sender;
        #endregion

        #region Action's
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByIdAsync(Guid PkId)
        {
            GetInvoiceDetailByIdQry Invoice = new()
            {
                PkInvoiceDetaiId = PkId
            };

            var Res = await Sender.Send(Invoice);
            return Result.Ok(Res);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync([FromQuery] Guid PkInvoiceId)
        {
            GetInvoiceDetailByInvoiceIdQry Invoice = new()
            {
                FkInvoiceId = PkInvoiceId
            };

            var Res = await Sender.Send(Invoice);
            return Result.Ok(Res);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> InsertAsync(CreateInvoiceDetailCmd Cmd)
        {
            CreateInvoiceDetailCmd Invoice = new()
            {
                Cost = Cmd.Cost,
                Count = Cmd.Count,
                FkInvoiceId = Cmd.FkInvoiceId,
                FkProductId = Cmd.FkProductId
            };

            var Validate = new CreateInvoiceDetailValidator().Validate(Invoice);

            if (!Validate.IsValid)
            {
                return Result.Ok(Validate.ToDictionary(), false);
            }

            var Res = await Sender.Send(Invoice);
            return Result.Ok(Res);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateAsync(UpdateInvoiceDetailCmd Cmd)
        {
            UpdateInvoiceDetailCmd Invoice = new()
            {
                PkInvoiceDetailId = Cmd.PkInvoiceDetailId,
                Cost = Cmd.Cost,
                Count = Cmd.Count,
                FkInvoiceId = Cmd.FkInvoiceId,
                FkProductId = Cmd.FkProductId
            };

            var Res = await Sender.Send(Invoice);
            return Result.Ok(Res);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> DeleteAsync([FromQuery] Guid PkId)
        {
            DeleteInvoiceDetailCmd Invoice = new()
            {
                PkInvoiceDetailId = PkId
            };

            var Res = await Sender.Send(Invoice);
            return Result.Ok(Res);
        }
        #endregion
    }
}
