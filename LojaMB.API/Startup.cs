using LojaMB.API.Context;
using LojaMB.API.Seed;
using LojaMB.API.Services;
using LojaMB.API.Services.Inteface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace LojaMB.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddMvc().AddNewtonsoftJson(options =>
                        {
                            options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        });

            services.AddDbContext<ApiContext>(options =>
                options.UseInMemoryDatabase("InMemoryDatabase"));
            services.AddCors();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Loja MB",
                    Description = "Api Testing"  
               });
            });

            services.AddApiVersioning(options =>
            {
                options.ApiVersionReader = new QueryStringApiVersionReader();
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
            });

            services.AddTransient<IProdutoService, ProdutoService>();
            //services.AddScoped<UsuarioService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            
            ProdutosSeed.Initialize(app.ApplicationServices);

            app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Loja MB");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
