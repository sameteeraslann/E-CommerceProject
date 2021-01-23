using CMSProject.Data.Context;
using CMSProject.Data.Repositories.Concrete.EntityTypeRepositories;
using CMSProject.Data.Repositories.Interfaces.EntityTypeRepositories;
using CMSProject.Entity.Entities.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSProject.Web
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
            services.AddMemoryCache(); //Github April 4, bir makalede gereksiz olduðunu gördüm
            services.AddSession(opt =>
            {
                //opt.IdleTimeout = TimeSpan.FromDays(30);
                //opt.IdleTimeout = TimeSpan.FromSeconds(10);
            });
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddControllersWithViews();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPageRepository, PageRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 3;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
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

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();//Session iþlemleri için gelen requesleri handler ederken bu middleware uðramak zorunda

            app.UseAuthentication(); //Gelen requestler yetkilerden önce sisteme aythentication olmak zorunda bu yüzden "UseAuthentication" middleware buraya ekledik.
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                // NOT : => DEFAULT ÜZERÝNDE YAPILACAK ÝÞLEMLER ÝÇÝN TEK TEK ENDPOÝNT KULLANILMASI GEREKÝYOR. FAKAT AREA ÝÇÝN TEK BÝR  HAMLE YETERLÝDÝR.


                endpoints.MapControllerRoute(
                     name: "page", //Name => Yolun Adý
                     pattern: "{slug?}", // "slug?" nedir slug yanýnda id de taþýr... => pattern Bunun yazýlmasýnýn sebebi methodlarýn yapýlacaðý iþleme göre URL belirlenmesidir.
                defaults: new { controller = "Page", action = "Page" });  // => 

                endpoints.MapControllerRoute(
                name: "product",
                pattern: "product/{categorySlug}",
                defaults: new { controller = "Product", action = "ProductByCategory" }); // => endpointleri methodlara yönlendirmek için default kullanýlýr.

                endpoints.MapControllerRoute( // => default sayfalar için bu end pointi kullandýk
                name: "default", //=> yolun adý
                pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute( //=> Area içerisinde bütün controller üzerinde ki methodlarýn olduðu View sayfasýnýn görüntülenmesi için sadece bu endpointi kullanmak yeterlidir.
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"); // => pattern adres gösterir, eðer direk adres belirtiyorsa defaults a gerek yok. 
                // exist => kullanýldýðýnda name içerisinde yazýlý olan bütün index ve methodlar çalýþýr.
                // sað tarafýna yazýlmasý gereken adreslerin "{controller=Home}/{action=Index}/{id?}" standartý belirtilmesi yeterlidir. 
            });
        }
    }
}

