using TaxInvoicesWebApi.AggregatesModel.BillAgregate;
using TaxInvoicesWebApi.AggregatesModel.CustomerAgregate;
using TaxInvoicesWebApi.AggregatesModel.ProductAgregate;
using TaxInvoicesWebApi.ConnectionSql;
using TaxInvoicesWebApi.Infrastructure.Finder;
using TaxInvoicesWebApi.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IDatabaseConnection, DatabaseConnection>();
builder.Services.AddScoped<ICustomerFinder, CustomerFinder>();
builder.Services.AddScoped<IProductFinder, ProductFinder>();
builder.Services.AddScoped<IBillFinder, BillFinder>();
builder.Services.AddScoped<IBillRepository, BillRepository>();
builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
    }
);

var app = builder.Build();
app.UseCors();

if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
