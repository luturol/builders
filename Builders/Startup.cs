using Builders.Config;
using Builders.Interfaces;
using Builders.Repository;
using Builders.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Builders
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
             services.AddHttpsRedirection(options =>
            {
                options.HttpsPort = 443;
            });

            services.Configure<BinarySearchTreeDbSettings>(Configuration.GetSection(nameof(BinarySearchTreeDbSettings)));
            services.AddSingleton<IMongoDbSettings>(sp => sp.GetService<IOptions<BinarySearchTreeDbSettings>>().Value);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Builders", Version = "v1" });
            });

            services.AddTransient<IBinarySearchTreeRepository, BinarySearchTreeRepository>();
            services.AddTransient<IBinarySearchTreeService, BinarySearchTreeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Builders v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
