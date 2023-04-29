using System.Net.Http.Headers;
using JWT.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var helper = new Helper();
string accessToken = await helper.RetrieveAccessTokenAsync(builder.Configuration["ClientId"], 
    builder.Configuration["ClientSecret"], 
    builder.Configuration["BaseUrl"]);

builder.Services.AddHttpClient("requestHandler",(serviceProvider, httpClient) =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration["BaseUrl"] ?? "");
    var accessToken = helper.RetrieveAccessTokenAsync(builder.Configuration["ClientId"], 
        builder.Configuration["ClientSecret"], 
        builder.Configuration["BaseUrl"]).GetAwaiter().GetResult();
    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
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
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

