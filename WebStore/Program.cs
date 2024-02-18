using WebStore.Data.Entities.Account;
using WebStore.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.Data;
using WebStore.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention());
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add Config for Required Email
builder.Services.Configure<IdentityOptions>(options => options.SignIn.RequireConfirmedEmail = true);
//

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
});

//Add Email Configs
var emailConfig = builder.Configuration.GetSection("EmailConfiguration")
.Get<EmailConfiguration>();

builder.Services.AddSingleton(emailConfig);

//builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddScoped<IEmailService, EmailService>();
//

builder.Services.AddScoped<ProductService, ProductService>();
builder.Services.AddScoped<CategoryService, CategoryService>();
builder.Services.AddScoped<CommonService, CommonService>();
builder.Services.AddScoped<ReviewService, ReviewService>();
builder.Services.AddScoped<CartService, CartService>();
builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();


app.Run();