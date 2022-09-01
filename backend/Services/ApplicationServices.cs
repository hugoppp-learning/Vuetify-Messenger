using backend.Repository;

namespace backend.Services;

public static class ApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<UserRepo>()
            .AddSingleton<AuthService>()
            .AddSingleton<DevelopmentDataService>();
    }
    
}