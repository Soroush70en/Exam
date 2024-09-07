using Domain.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services, string ConnectionString)
    {
        string ExamDbContext = ConnectionString;
        services.AddDbContext<ExamDbContext>(options => options.UseSqlServer(ExamDbContext).UseLazyLoadingProxies());
        return services;
    }
};
