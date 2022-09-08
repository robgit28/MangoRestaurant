using Mango.Server.Identity;
using Mango.Server.Identity.DbContexts;
using Mango.Server.Identity.Initializer;
using Mango.Server.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// get the connection string for DB setup / connection 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(connectionString));

// the in-built aspnet identity (seperate to Identity Server)
// takes 2 parameters - the user & the default role 
// AddDefaultTokenProviders - token generated when user forgets password 
// referenced in DbInitializer 
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();


var identityBuilder = builder.Services.AddIdentityServer(options =>
    {
        // logging information 
        options.Events.RaiseErrorEvents = true;
        options.Events.RaiseInformationEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseSuccessEvents = true;
        options.EmitStaticAudienceClaim = true;
    }
).AddInMemoryIdentityResources(Constants.IdentityResources)
.AddInMemoryApiScopes(Constants.ApiScopes)
.AddInMemoryClients(Constants.Clients)
.AddAspNetIdentity<ApplicationUser>();

// generates a key for dev purposes 
identityBuilder.AddDeveloperSigningCredential();
//configure user roles & claims 
builder.Services.AddScoped<IDbInitializer, DbInitializer>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// adds the following to the pipeline 
// redirects any http requests to https (secure network) - recommended for prod apps 
app.UseHttpsRedirection();
// serves static files such as html, JS, CSS, image files without any server-side processing
app.UseStaticFiles();

app.UseRouting();
// adds Identity Server to the pipeline 
app.UseIdentityServer(); 
app.UseAuthorization();

SeedDatabase(); 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

void SeedDatabase()
{
    var context = app.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();
    var userManager = app.Services.CreateScope().ServiceProvider.GetService<UserManager<ApplicationUser>>();
    var roleManager = app.Services.CreateScope().ServiceProvider.GetService<RoleManager<IdentityRole>>();

    var dbInitializer = new DbInitializer(context, userManager, roleManager);
    dbInitializer.Initialize(); 
}
