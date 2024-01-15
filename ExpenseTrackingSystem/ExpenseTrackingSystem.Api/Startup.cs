using ETS.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackingSystem.Api;
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
        services.AddControllers();

        // Entity Framework Core için DbContext'i ekleyin
        services.AddDbContext<ETSDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("MsSqlConnection")));

        // Diğer servisleri ekleyebilirsiniz.
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
            // Hata sayfalarını özelleştirmek istiyorsanız, uygun bir sayfa yolunu belirtin.
            // app.UseHsts();
        }
        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}

