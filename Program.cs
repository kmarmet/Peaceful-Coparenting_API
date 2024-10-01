using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
var CustomOrigins = "Custom";
builder.Services.Configure<IISServerOptions>(options =>
{
    options.AutomaticAuthentication = false;
});
builder.Services.AddCors(options =>
    options.AddPolicy(name: "Custom",
    policy =>
    {
        policy.WithOrigins().AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).AllowCredentials();
        policy.WithHeaders("accept", "content-type", "origin", "X-InlineCount");
    })
);
var app = builder.Build();
//builder.WebHost.UseUrls("https://*:5000");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(new ExceptionHandlerOptions()
    {
        ExceptionHandlingPath = "/error"
    });
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();
//app.UseCors("PCP Domain");
//app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Messaging}/{action=Index}/{id?}").RequireCors(CustomOrigins);
app.MapControllerRoute(
    name: "document",
    pattern: "{controller=Document}/{action=Index}/{id?}").RequireCors(CustomOrigins);
app.Run();
