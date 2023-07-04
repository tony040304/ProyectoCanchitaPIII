using Services.IServices;
using Services.Service;

namespace ProyectoCanchitaPIII
{
    public static class CompositeRoot
    {
        public static void DependencyInjection(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICanchitaGolServices, CanchitaGolServices>();
            builder.Services.AddScoped<IUsersService, UsuarioServices>();
        }
    }
}
