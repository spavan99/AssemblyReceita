using Assembly.Database;
using Assembly.Domain;
using Assembly.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.CrossCutting
{
    public static class IOCInjection
    {

        public static void AddRepos(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IDificuldadeRepository, DificuldadeRepository>();
            services.AddScoped<IReceitaRepository, ReceitaRepository>();
            services.AddScoped<IItensReceitaRepository, ItensReceitaRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped< IAuthService, AuthService>();
            services.AddScoped<IUserResolverService, UserResolverService>();


            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IDificuldadeService, DificuldadeService>();
            services.AddScoped<IReceitaService, ReceitaService>();
            services.AddScoped<IItensReceitaService, ItensReceitaService>();


        }
    }
}
