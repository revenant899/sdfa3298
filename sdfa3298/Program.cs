using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using sdfa3298;
using sdfa3298.Initializer;
using sdfa3298.Models;
using sdfa3298.Repositories.Categories;
using sdfa3298.Repositories.Products;
using sdfa3298.Services;
using sdfa3298.ViewModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add database context
builder.Services.AddDbContext<AppDbContext>(options =>
{
    string? connectionString = builder.Configuration.GetConnectionString("LocalDb");
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();


builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>

{

    options.User.RequireUniqueEmail = true;

    options.Password.RequiredUniqueChars = 1;

    options.Password.RequireNonAlphanumeric = true;

    options.Password.RequiredLength = 8;

    options.Password.RequireUppercase = true;

    options.Password.RequireLowercase = true;

    options.Password.RequireDigit = true;

})

    .AddEntityFrameworkStores<AppDbContext>()

    .AddDefaultTokenProviders()

    .AddDefaultUI();


// Add repositories

// ������� � Dependency injection �� ������� Singleton
// ��'��� ����� ���� �������� � ������ ���������
//builder.Services.AddSingleton<CategoryRepository>();

// ��'��� ���� ������������ ��� ������� ������������
//builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();

// ��'��� ���� ������������ ��� ������� HTTP ������
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();


builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.Configure<PaginationVM>(builder.Configuration.GetSection("Pagination"));

// Add session
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
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
app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Seed();

app.Run();