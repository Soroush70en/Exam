﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// مدل فروشنده
    /// </summary>
    [Table($"tb{nameof(Seller)}s", Schema ="mis")]
    public record Seller : Entity
    {
        #region Property's
        /// <summary>
        /// نام
        /// </summary>
        [StringLength(200)]
        public string Firstname { get; set; }

        /// <summary>
        /// نام خانوادگی
        /// </summary>
        [StringLength(200)]
        public string Lastname { get; set; }
        #endregion

        #region NotMapped's
        /// <summary>
        /// نام کامل
        /// </summary>
        public virtual string Fullname => $"{Firstname} {Lastname}";
        #endregion
    }
}
