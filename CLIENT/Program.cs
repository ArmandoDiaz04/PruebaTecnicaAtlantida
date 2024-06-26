using CLIENT.Services.Contracts;
using CLIENT.Services;
using System.ComponentModel;
using OfficeOpenXml;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IServiceTarjeta, ServiceTarjeta>();
builder.Services.AddScoped<IServiceMovimientosTarjeta, ServiceMovimientosTarjeta>();





var app = builder.Build();

ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;



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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
