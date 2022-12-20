using AutoMapper;
using Common.Bll.Helpers;
using Data.EF.Contexts;
using GraphicInterface.Common;
using GraphicInterface.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json.Serialization;

namespace GraphicInterface
{
    public sealed class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddNewtonsoftJson(options => { options.SerializerSettings.ContractResolver = new DefaultContractResolver(); });

            services.AddCors(x => x.AddDefaultPolicy(builder => { builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); }));

            services.AddSession();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<SessionHelper>();

            foreach (var item in AutofacHelper.GetTypes()) {
                services.AddScoped(item.Key, item.Value);
            }

            services.AddSingleton<IMapper>(_ => MapperProfile.GetMapperConfiguration().CreateMapper());

            services.AddDbContext<ShopContext>(builder => { builder.UseSqlServer(Configuration.GetConnectionString("ShopEntities")); });

            services.AddDataProtection()
                .SetApplicationName("YourAppName")
                .SetDefaultKeyLifetime(TimeSpan.FromDays(180));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            
            app.UseSession();

            app.UseCors();
            app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
        }
    }
}