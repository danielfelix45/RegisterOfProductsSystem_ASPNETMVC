using Microsoft.EntityFrameworkCore;
using RegisterOfProducts.Data;
using RegisterOfProducts.Helper;
using RegisterOfProducts.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure DB
builder.Services.AddDbContext<ProductDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserSession, UserSession>();
builder.Services.AddScoped<IEmail, Email>();

builder.Services.AddSession(o =>
{
    o.Cookie.HttpOnly= true;
    o.Cookie.IsEssential= true;
});

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
app .UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
