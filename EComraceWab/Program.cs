using EComraceWab.Helpers;
using EComraceWab.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using EComraceWab.Entites;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
{
    var services = builder.Services;
    services.AddCors();
    services.AddControllers()
        .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null)
        .AddRazorRuntimeCompilation();

    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
    services.AddMvc(options => options.EnableEndpointRouting = false);

    services.AddScoped<IUserService, UserService>();
    services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();
}
builder.Services.AddDbContext<DarazContext>(options => options.UseSqlServer
(builder.Configuration.GetConnectionString("DefaultConnection")));



var app = builder.Build();

{

    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    app.UseMiddleware<JwtMiddleware>();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
    }
    app.UseHttpsRedirection();

    app.UseStaticFiles();


    app.UseRouting();


    app.MapControllers();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(name: "default", pattern: "{controller=Daraz}/{action=Home}/{id?}");
    });
}

app.Run();