using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

/// <summary>
/// مدل تخفیفات
/// </summary>
[Table($"tb{nameof(Discount)}s", Schema ="fin")]
public record Discount : Entity
{
    #region Property's
    /// <summary>
    /// مبلغ
    /// </summary>
    public long Price { get; set; }

    /// <summary>
    /// نوع تخفیف
    /// </summary>
    public DiscountType DiscountType { get; set; } = DiscountType.Documental;
    #endregion

    #region NotMapped's
    public virtual string DiscountTypeStr => DiscountType == DiscountType.Documental ? "سندی" : "ردیفی";
    #endregion

    #region Foreign Key's
    /// <summary>
    /// شناسه هدر پیش فاکتور
    /// </summary>
    public Guid? FkInvoiceId { get; set; }

    [ForeignKey(nameof(FkInvoiceId))]
    public virtual Invoice Invoice { get; set; }

    /// <summary>
    /// شناسه جزئیات پیش فاکتور
    /// </summary>
    public Guid? FkInvoiceDetialId { get; set; }

    [ForeignKey(nameof(FkInvoiceDetialId))]
    public virtual InvoiceDetail InvoiceDetail { get; set; }
    #endregion
}
