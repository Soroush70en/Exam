using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

/// <summary>
/// مدل میانی لاین فروش و محصولات
/// </summary>
[Table($"tb{nameof(SellLineProduct)}s", Schema ="shp")]
public record SellLineProduct : Entity
{
    #region Foreign Key's
    /// <summary>
    /// شناسه لاین فروش
    /// </summary>
    public Guid FkSellLineId { get; set; }

    [ForeignKey(nameof(FkSellLineId))]
    public virtual SellLine SellLine { get; set; }

    /// <summary>
    /// شناسه محصول
    /// </summary>
    public Guid FkProductId { get; set; }

    [ForeignKey(nameof(FkProductId))]
    public virtual Product Product { get; set; }
    #endregion
}
