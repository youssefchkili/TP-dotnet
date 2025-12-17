using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MyFirstApp.Models;
using MyFirstApp.Interceptors;
using MyFirstApp.Services;
using MyFirstApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Ajouter Razor Pages pour Identity UI
builder.Services.AddRazorPages();

// Configure DbContext avec l'intercepteur d'audit
builder.Services.AddDbContext<ApplicationdbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .AddInterceptors(new AuditLogInterceptor())); // Ajouter l'intercepteur

// Configurer Identity avec DefaultIdentity
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Pas besoin de confirmer le compte
})
.AddEntityFrameworkStores<ApplicationdbContext>();

// Configuration des options d'Identity
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
    
    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.AllowedForNewUsers = true;
    
    // User settings.
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

// Register Generic Repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Register Specific Repositories
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();

// Register Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register Services
builder.Services.AddScoped<MovieService>();

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

// IMPORTANT: L'ordre est crucial !
// 1. Authentication (qui es-tu ?)
app.UseAuthentication();
// 2. Authorization (que peux-tu faire ?)
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Mapper les Razor Pages pour Identity
app.MapRazorPages();

app.Run();
