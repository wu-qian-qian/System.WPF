using Commons;
using System.Api.Module;
using System.Api.Options;

namespace System.Api
{
    public static class Startup
    {
        /// <summary>
        /// ≥ı ºªØ
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static WebApplicationBuilder Initialize(this  WebApplicationBuilder builder)
        {
            LogOptions logOptions = new LogOptions() { IsRecordGet = false };
            builder.Services.AddSingleton(logOptions);
            var assemblieList = ReflectionHelper.GetAllReferencedAssemblies();
            builder.AddSerilog();
            builder.Services.AddAutoMapping();
            builder.Services.AddFluentValidationExpand(assemblieList);
            return builder;
        }
    }
}
