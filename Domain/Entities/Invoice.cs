using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// مدل هدر فاکتور
    /// </summary>
    [Table($"tb{nameof(Invoice)}s", Schema ="fin")]
    public record Invoice : Entity
    {
        #region Property's
        /// <summary>
        /// وضعیت فاکتور
        /// </summary>
        public InvoiceStatus InvStatus { get; set; } = InvoiceStatus.Draft;
        #endregion

        #region NotMapped's
        /// <summary>
        /// جزئیات فاکتور
        /// </summary>
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }

        /// <summary>
        /// لیست تخفیفات
        /// </summary>
        public virtual ICollection<Discount> Discounts { get; set; }

        /// <summary>
        /// وضعیت فارسی
        /// </summary>
        public virtual string InvStatusStr => InvStatus == InvoiceStatus.Draft ? "پیش نویس" : "نهایی";
        #endregion

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


        /// <summary>
        /// شناسه مشتری
        /// </summary>
        public Guid FkCustomerId { get; set; }
        [ForeignKey(nameof(FkCustomerId))]
        public virtual Customer Customer { get; set; }
        #endregion
    }
}
