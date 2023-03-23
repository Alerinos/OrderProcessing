using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using OrderProcessing.Server;
using OrderProcessing.Server.Modules;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScoped<OrderProcessing.Server.Modules.Order>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* Initialize */
// Yes, I know it can be done better....
var serviceProvider = builder.Services?.BuildServiceProvider();
var context = serviceProvider?.GetService<Context>();
Context.Initialize(context ?? throw new Exception("Context is not registered."));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

RouteGroupBuilder order = app.MapGroup("/order");

order.MapGet("/", async (OrderProcessing.Server.Modules.Order order) =>
{
    return Results.Ok(await order.GetAllAsync());
});

order.MapGet("/{id}", async (Guid id, OrderProcessing.Server.Modules.Order order) =>
{
    return Results.Ok(await order.GetAsync(id));
});

order.MapDelete("/{id}", async (Guid id, OrderProcessing.Server.Modules.Order order) =>
{
    return Results.Ok(await order.DeleteAsync(id));
});

order.MapPost("/create", async (string client, List<OrderProcessing.Shared.DTO.OrderLine> lines, string? note, OrderProcessing.Server.Modules.Order order) => 
{
    var result = await order.CreateAsync(client, lines, note);
    return Results.Ok(result);
});

order.MapPost("/update/{id}/{type}", async (Guid id, string type, string value, OrderProcessing.Server.Modules.Order order) => 
{
    return type switch
    {
        "client" => Results.Ok(await order.UpdateClientAsync(id, value)),
        "note" => Results.Ok(await order.UpdateNoteAsync(id, value)),
        "status" => Results.Ok(await order.UpdateStatusAsync(id, value)),
        _ => Results.NotFound(),
    };
});

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();