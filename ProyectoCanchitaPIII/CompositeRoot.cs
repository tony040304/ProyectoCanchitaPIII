using Services.IServices;
using Services.Service;

namespace ProyectoCanchitaPIII
{
    public static class CompositeRoot
    {
        public static void DependencyInjection(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAuthenticate, AuthenticateServices>();
            builder.Services.AddScoped<ICanchitaServices, CanchitaServices>();
            builder.Services.AddScoped<IUsersService, UsuarioServices>();
        }
    }
}
