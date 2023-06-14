using SignalRServer;

var builder = WebApplication.CreateBuilder(args);

//Allow CORS to accept requests from *any* origin
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
           .AllowAnyMethod()
                  .AllowAnyHeader()
                         .AllowCredentials()
                                .SetIsOriginAllowed((host) => true));
});
// Add services to the container.

//builder.Services.AddRazorPages();

builder.Services.AddSignalR();




var app = builder.Build();

app.UseCors(x => x
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/chathub");
});

//app.UseAuthorization();

//app.MapRazorPages();

app.Run();
