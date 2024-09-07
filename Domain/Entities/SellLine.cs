using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

/// <summary>
/// مدل لاین فروش
/// </summary>
[Table($"tb{nameof(SellLine)}s", Schema ="shp")]
public record SellLine : Entity
{
    #region Property's
    /// <summary>
    /// عنوان
    /// </summary>
    [StringLength(200)]
    public string Title { get; set; }
    #endregion
}
