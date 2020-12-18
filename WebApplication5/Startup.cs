using GoogleReCaptcha.V3;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication5.Services;

namespace WebApplication5
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
            //services.AddMemoryCache().AddSimpleCaptcha(builder =>
            //{
            //    builder.UseMemoryStore();
            //});

            services.AddHttpClient<ICaptchaValidator, GoogleReCaptchaValidator>();

            services.AddOptions();
            
            ////�qappsettings.json��������t�m
            //services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
            
            ////�`�J�p�ƾ��M�W�h�x�s
            //services.AddSingleton<IIpPolicyStore, DistributedCacheIpPolicyStore>();
            //services.AddSingleton<IRateLimitCounterStore, DistributedCacheRateLimitCounterStore>();
            
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            ////�t�m�]�p�ƾ����_�ͦ����^
            //services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            
            services.AddSingleton<ITicketService, TicketService>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //app.UseIpRateLimiting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Ticket}/{action=Index}/{id?}");
            });
        }
    }
}
