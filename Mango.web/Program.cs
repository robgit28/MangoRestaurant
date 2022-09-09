using Mango.web;
using Mango.web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//HttpClient
builder.Services.AddHttpClient<IProductService, ProductService>();
Constants.ProductAPIBase = builder.Configuration["ServiceUrls:ProductAPI"];
// Add Product service 
builder.Services.AddScoped<IProductService, ProductService>();
// Identity Server Authentication
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "oidc";
    })
    // cookies expires after 10 mins 
    .AddCookie("Cookies", c => c.ExpireTimeSpan=TimeSpan.FromMinutes(10))
    .AddOpenIdConnect("oidc", options =>
    {
        // from appsettings - IdentityAPI - see Constants reference above
        options.Authority = builder.Configuration["ServiceUrls:IdentityAPI"];
        // see ClientId in Constants file in Identitty Project  
        options.ClientId = "mango";
        // see ClientSecret in Constants file in Identitty Project  
        options.ClientSecret = "secret";
        // see AllowedGrantTypes in Constants file in Identitty Project  
        options.ResponseType = "code";
        options.TokenValidationParameters.NameClaimType = "name"; 
        options.TokenValidationParameters.RoleClaimType = "role"; 
        //options.Scope.Add("openid");
        //options.Scope.Add("profile");
        // see APIScope (name) in Constants file in Identitty Project - new ApiScope("mango", "Mango Server."),
        options.Scope.Add("mango");
        options.SaveTokens = true;
        //options.GetClaimsFromUserInfoEndpoint = true;
        //options.ClaimActions.MapJsonKey("role", "role", "role");
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// redirects any http requests to https (secure network) - recommended for prod apps 
app.UseHttpsRedirection();
// serves static files such as html, JS, CSS, image files without any server-side processing
app.UseStaticFiles();

app.UseRouting();

// UseAuthentication always before UseAuthorization
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
