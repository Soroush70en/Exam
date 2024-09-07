using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

/// <summary>
/// مدل فاکتور
/// </summary>
[Table($"tb{nameof(InvoiceDetail)}s", Schema ="fin")]
public record InvoiceDetail : Entity
{
    #region Property's
    /// <summary>
    /// تعداد
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// مبلغ
    /// </summary>
    public long Cost { get; set; }
    #endregion

    #region NotMapped's
    public virtual ICollection<Discount> Discounts { get; set; }
    #endregion

    #region Foreign Key's
    /// <summary>
    /// شناسه محصول
    /// </summary>
    public Guid FkProductId { get; set; }

    [ForeignKey(nameof(FkProductId))]
    public virtual Product Product { get; set; }


    /// <summary>
    /// شناسه هدر فاکتور
    /// </summary>
    public Guid FkInvoiceId { get; set; }

    [ForeignKey(nameof(FkInvoiceId))]
    public virtual Invoice Invoice { get; set; }
    #endregion
}
