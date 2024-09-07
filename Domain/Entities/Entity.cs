using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

/// <summary>
/// مدل پایه
/// </summary>
public record Entity
{
    #region Property's
    /// <summary>
    /// کلید انحصاری
    /// </summary>
    [Key]
    public Guid PkId { get; set; }

    /// <summary>
    /// وضعیت رکورد
    /// </summary>
    public byte Status { get; set; } = 1;

    /// <summary>
    /// زمان ثبت رکورد
    /// </summary>
    public DateTimeOffset CreateAt { get; set; } = DateTimeOffset.UtcNow;
    #endregion
}
