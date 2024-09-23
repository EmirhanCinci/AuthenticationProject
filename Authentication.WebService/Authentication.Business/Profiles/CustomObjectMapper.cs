using AutoMapper;

namespace Authentication.Business.Profiles
{
    public static class CustomObjectMapper
    {
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var configuration = new MapperConfiguration(config =>
            {
                config.AddProfile<UserProfile>();
                config.AddProfile<RoleProfile>();
                config.AddProfile<UserRoleProfile>();
            });
            return configuration.CreateMapper();
        });

        public static IMapper Mapper => lazy.Value;
    }
}
