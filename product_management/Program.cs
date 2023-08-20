using Microsoft.EntityFrameworkCore;
using product_management.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ProductDbContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("MyConnection")));
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDistributedRedisCache(options =>
{
    options.Configuration = "localhost:6379,password=123456";
    options.InstanceName = "test";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");
app.Run();