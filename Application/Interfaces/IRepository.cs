using System.Linq.Expressions;

namespace Application.Interfaces;

/// <summary>
/// ریپوزیتوری اتصال به جدول دیتابیس
/// </summary>
/// <typeparam name="TEntity">موجودیت</typeparam>
public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// تابع درج رکورد
    /// </summary>
    /// <param name="Entry">مدل جدول دیتابیس</param>
    Task InsertAsync(TEntity Entry);

    /// <summary>
    /// تابع درج رکورد
    /// </summary>
    /// <param name="Entry">مدل جدول دیتابیس</param>
    Task InsertRangeAsync(IEnumerable<TEntity> Entry);

    /// <summary>
    /// تابع دریافت یک رکورد خاص
    /// </summary>
    /// <param name="Filter">فیلتر داده</param>
    /// <returns></returns>
    Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> Filter = null);

    /// <summary>
    /// تابع دریافت تمام رکوردهای جدول
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> GetAllAsync();

    /// <summary>
    /// تابع دریافت اولین رکورد جدول
    /// </summary>
    /// <returns>مدل جدول دیتابیس</returns>
    Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> Filter = null);

    /// <summary>
    /// تابع دریافت رکوردهای جدول
    /// </summary>
    /// <param name="Filter">فیلتر داده</param>
    /// <param name="OrderBy">مرتب‌سازی بر مبنای یک فیلد</param>
    /// <returns>لیست رکوردهای دیتابیس</returns>
    Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> Filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy = null, int? Page = null, int? PerPage = null);

    /// <summary>
    /// تابع دریافت تعداد رکوردهای جدول
    /// </summary>
    /// <param name="Filter">فیلتر داده</param>
    /// <returns></returns>
    Task<int> GetCountAsync(Expression<Func<TEntity, bool>> Filter = null);

    /// <summary>
    /// تابع یافتن یک رکورد
    /// </summary>
    /// <param name="Id">آیدی رکورد</param>
    /// <returns>مدل جدول دیتابیس</returns>
    Task<TEntity> FindAsync(object Id);

    /// <summary>
    /// تابع ویرایش رکورد بر اساس مدل وروردی
    /// </summary>
    /// <param name="Entry">مدل جدول دیتابیس</param>
    Task UpdateAsync(TEntity Entry);

    /// <summary>
    /// تابع ویرایش رکورد بر اساس مدل وروردی
    /// </summary>
    /// <param name="Entry">مدل جدول دیتابیس</param>
    Task UpdateRangeAsync(IEnumerable<TEntity> Entrys);

    /// <summary>
    /// تابع حذف رکورد براساس آیدی ورودی
    /// </summary>
    /// <param name="Id">آیدی رکورد</param>
    Task DeleteAsync(object Id);

    /// <summary>
    /// تابع حذف دسته‌ای رکوردها براساس مدل ورودی
    /// </summary>
    /// <param name="Entry"></param>
    Task DeleteRangeAsync(IEnumerable<TEntity> Entrys);
}
