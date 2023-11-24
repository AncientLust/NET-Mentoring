using Microsoft.EntityFrameworkCore;
using MVC_Principles.contexts;
using MVC_Principles.Settings;

namespace MVC_Principles
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.Configure<ProductSettings>(builder.Configuration.GetSection("ProductSettings"));
            builder.Services.AddDbContext<SqlServerContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerString")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});

			app.Run();
        }
    }
}