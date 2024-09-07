using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

/// <summary>
/// مدل میانی لاین فروش و محصولات
/// </summary>
[Table($"tb{nameof(SellLineSeller)}s", Schema ="shp")]
public record SellLineSeller : Entity
{
    #region Foreign Key's
    /// <summary>
    /// شناسه لاین فروش
    /// </summary>
    public Guid FkSellLineId { get; set; }

    [ForeignKey(nameof(FkSellLineId))]
    public virtual SellLine SellLine { get; set; }

    /// <summary>
    /// شناسه فروشنده
    /// </summary>
    public Guid FkSellerId { get; set; }

    [ForeignKey(nameof(FkSellerId))]
    public virtual Seller Seller { get; set; }
    #endregion
}
