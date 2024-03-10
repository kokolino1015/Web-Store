using WebStore.Data.Entities.Account;
using WebStore.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.Data;
using WebStore.Services;
using Stripe;
using Stripe.Issuing;

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
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

/*
// Temporary turn off EmailConfirmation
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
*/

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
builder.Services.AddSingleton<ChargeService>(new ChargeService());
builder.Services.AddSingleton<TransactionService>(new TransactionService());
builder.Services.AddScoped<WebStore.Services.ProductService, WebStore.Services.ProductService>();
builder.Services.AddScoped<CategoryService, CategoryService>();
builder.Services.AddScoped<CommonService, CommonService>();
builder.Services.AddScoped<WebStore.Services.ReviewService, WebStore.Services.ReviewService>();
builder.Services.AddScoped<CartService, CartService>();
builder.Services.AddScoped<WebStore.Services.AccountService, WebStore.Services.AccountService>();
builder.Services.AddControllersWithViews();

StripeConfiguration.SetApiKey(builder.Configuration["Stripe:TestSecretKey"]);

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