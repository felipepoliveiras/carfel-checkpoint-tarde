using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Senai.Carfel.Checkpoint
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Habilita o módulo de MVC para sua aplicação web
            services.AddMvc();

            //Habilita o uso de sessão
            services.AddSession(s => s.IdleTimeout = TimeSpan.FromMinutes(30));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Para habilitar o uso da pasta /wwwroot
            //app.UseStaticFiles();

            //Utiliza o serviço de sessão
            app.UseSession();

            //Habilita o uso do /wwwroot
            app.UseStaticFiles();

            //Utiliza o serviço de MVC
            app.UseMvc(
                rotas =>
                    rotas.MapRoute(
                        name: "defaults",
                        template: "{controller=Pages}/{action=Index}"
                    )
            );
        }
    }
}
