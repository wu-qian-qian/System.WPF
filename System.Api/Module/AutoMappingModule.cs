using AutoMapper;

namespace System.Api.Module
{
    public class AutoMappingModule: Profile
    {
        public AutoMappingModule()
        {
            //CreateMap<User, UserMap>();
            //CreateMap<MomeEntity, MomeRespones>()
            //.ForMember(p => p.Title, options => options.MapFrom(p => p.Article.Title))
            //.ForMember(p => p.Message, options => options.MapFrom(p =>
            //p.Article.Data));
            //CreateMap<NoteBookEntity, MomeRespones>()
            //.ForMember(p => p.Title, options => options.MapFrom(p =>
            //p.Article.Title))
            //.ForMember(p => p.Message, options => options.MapFrom(p =>
            //p.Article.Data));
        }
    }
    public static class AutoMappingExpand
    {
        public static IServiceCollection AddAutoMapping(this IServiceCollection services)
        {
            var auto = new MapperConfiguration((config => {
                config.AddProfile(new AutoMappingModule());
            }));
            services.AddSingleton(auto.CreateMapper());
            return services;
        }
    }
}
