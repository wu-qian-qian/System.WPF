using FluentValidation.AspNetCore;
using System.Reflection;
namespace System.Api.Module
{
    public static class AddFluentValidationModule
    {
        public static  IServiceCollection AddFluentValidationExpand(this  IServiceCollection services,IEnumerable<Assembly> assemblies)
        {
            services.AddFluentValidation(opt =>
            {
                //该程序的dll
                opt.RegisterValidatorsFromAssemblies(assemblies);
            });
            return services;
        }
    }
}
