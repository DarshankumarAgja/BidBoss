using BidBoss.Data;
using BidBoss.Models; // ✅ Ensure ApplicationUser is included
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ Configure database connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Register Identity for `ApplicationUser`
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// ✅ Step 6: Add Authorization Policies for Auction Security
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SellerOnly", policy => policy.RequireRole("Seller"));
});

builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();

// ✅ Enable Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    // ✅ Ensure RoleManager & UserManager are available
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

    await RoleInitializer.InitializeRoles(services); // Ensure default roles exist
}

app.Run();
