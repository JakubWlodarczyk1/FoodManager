using System.Globalization;
using FoodManager.Application.Extensions;
using FoodManager.Infrastructure.Extensions;
using FoodManager.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddSwaggerGen();

var app = builder.Build();

var scope = app.Services.CreateScope();
var categorySeeder = scope.ServiceProvider.GetRequiredService<ProductCategorySeeder>();
await categorySeeder.Seed();

var identitySeeder = scope.ServiceProvider.GetRequiredService<IdentitySeeder>();
await identitySeeder.Seed();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.Use(async (context, next) =>
{
    if (context.Request.Cookies.TryGetValue("Language", out var cookie) && !string.IsNullOrEmpty(cookie))
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo(cookie);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(cookie);
    }
    else
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("pl");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("pl");
    }

    await next.Invoke();
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
