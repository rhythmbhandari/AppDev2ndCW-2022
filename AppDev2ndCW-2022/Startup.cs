using AppDev2ndCW.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AppDev2ndCW_2022
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataBaseContext>(options =>
            {
                // Configure the context to use Microsoft SQL Server.
                options.UseSqlServer(Configuration["ConnectionString"], sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
                });

            });

            /*  services.AddDbContext<DataBaseContext>(options =>
              {
                  var connectionString = Configuration.GetConnectionString("DataBaseContext");
                  options.UseSqlServer(connectionString);
              });*/

            services.AddMvc();
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddRazorPages();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

        }
        public void Configure(IApplicationBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        }
    }
}
