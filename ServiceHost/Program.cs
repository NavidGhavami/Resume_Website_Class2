using Microsoft.EntityFrameworkCore;
using Resume.Application.Services.Implementation.User;
using Resume.Application.Services.Interface.User;
using Resume.Domain.Context;
using Resume.Domain.Entities.User;
using Resume.Domain.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var mvcBuilder =  builder.Services.AddControllersWithViews();


#if DEBUG

mvcBuilder.AddRazorRuntimeCompilation();

#endif

#region General Services

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddTransient<IUserService, UserService>();

#endregion


#region Database Config

var connectionString = builder.Configuration.GetConnectionString("ResumeWebsite_2");

builder.Services.AddDbContext<DatabaseContext>(option =>
	option.UseSqlServer(connectionString), ServiceLifetime.Transient);

#endregion

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
