using Domain.Context;

namespace Application.Interfaces;

/// <summary>
/// کلاس مدیریت اتصالات دیتابیس
/// </summary>
public interface IUnitofWork : IDisposable
{
    /// <summary>
    /// تابع برگرداندن دستک دیتابیس
    /// </summary>
    /// <returns>شی دیتابیس</returns>
    ExamDbContext GetContext();

    /// <summary>
    /// تابع ذخیره تغییرات ناهمزمان
    /// </summary>
    Task<bool> SaveChangeAsync();

    /// <summary>
    /// تابع ساخت ریپوزیتوری
    /// </summary>
    /// <typeparam name="TEntity">مدل جدول دیتابیس</typeparam>
    /// <returns>ریپوزیتوری دستک مدل جدول دیتابیس</returns>
    IRepository<TEntity> Repository<TEntity>() where TEntity : class;
}
