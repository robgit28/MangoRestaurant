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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
