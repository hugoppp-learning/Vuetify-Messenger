using backend.Repository;
using backend.Services;

namespace backend;

public static class ApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<UserRepo>()
            .AddSingleton<SignupService>();
    }
    
}