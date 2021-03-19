using EmployeeMangement.Data;
using EmployeeMangement.Models;
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

namespace EmployeeMangement
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

            services.AddControllersWithViews();
            // services.AddMvc();
            //ass dependece injection relationship
            services.AddDbContextPool<AppDBContext>(options =>
                            options.UseSqlServer(Configuration.
                            GetConnectionString("EmployeeConnectionString")));
            services.Configure<IdentityOptions>(option =>
            {
                option.Password.RequiredLength = 6;
                option.Password.RequiredUniqueChars = 0;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;
            });
            services.AddAuthorization(option =>
            {
                option.AddPolicy("DeleteRole", police=>police.
                                  RequireClaim("RemovRole"));
            });
          //add identity
            ///add idenity 
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores< AppDBContext > ();
            // services.AddTransient<IEmployeeREpository, MOKEmployeeRepository>();

            services.AddScoped<IEmployeeREpository, SQLEmployeeRepository>();

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
                //app.UseExceptionHandler("/Error");
               // app.UseStatusCodePagesWithReExecute("Error/{0}");
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            //add identiity UseAuthorization before route
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //////convantinal routing
                endpoints.MapControllerRoute(
                    name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");
                //to use attribute routing
               //0 endpoints.MapControllers();
            });
        }
    }
}
