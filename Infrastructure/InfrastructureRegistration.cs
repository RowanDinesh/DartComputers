using Domain.Web.Contracts;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>) , typeof(GenericRepository<>));
            services.AddScoped<IBrandRepository ,  BrandRepository>();
            services.AddScoped<ICategoryRepository , CategoryRepository>();
            services.AddScoped<IProductRepository , ProductRepository>();
            return services;
        }
    }
}
