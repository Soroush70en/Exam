using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

/// <summary>
/// مدل محصولات
/// </summary>
[Table($"tb{nameof(Product)}s", Schema ="shp")]
public record Product : Entity
{
    #region Property's
    /// <summary>
    /// عنوان
    /// </summary>
    [StringLength(200)]
    public string Title { get; set; }
    #endregion
}
