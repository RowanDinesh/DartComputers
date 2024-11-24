using Application.Web.Common;
using Application.Web.Services;
using Application.Web.Services.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Web
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection AddApplicationRegistration(this IServiceCollection services) 
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped(typeof(IPaginationServices<,>), typeof(PaginationServices<,>));
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBrandServices, BrandServices>();
            services.AddScoped<ICategoryServices, CategoryServices>();
            services.AddScoped<IProductServices, ProductServices>();
            return services;
            

        }
    }
}
