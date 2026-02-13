using OrdersSecurity;
using OrdersWeb.Data;
using OrdersWeb.SecurityDemos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSingleton<OrdersRepository>();
builder.Services.AddSingleton<FilesDemoService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// INTENTIONALLY BAD (hardcoded secret) - fix task will externalize it
app.Configuration["Payments:ApiKey"] = "sk_live_123456789";

app.MapGet("/api/orders/search", async (string customer, OrdersRepository repo) =>
{
    // VULNERABLE SQL injection path (task will switch to safe method)
    var rows = await repo.SearchOrdersVulnerable(customer);
    return Results.Ok(rows);
});

app.MapGet("/api/files/read", (string name, FilesDemoService files) =>
{
    // VULNERABLE path traversal (task will sanitize/allowlist)
    var text = files.ReadFileVulnerable(name, app.Environment.ContentRootPath);
    return Results.Text(text, "text/plain");
});

app.MapPost("/api/json/deserialize", async (HttpRequest req) =>
{
    // VULNERABLE insecure deserialization pattern via Json.NET TypeNameHandling
    using var reader = new StreamReader(req.Body);
    var payload = await reader.ReadToEndAsync();

    return Results.Ok(InsecureJson.Deserialize(payload));
});

// Weak crypto reachable via API (so the library is used)
app.MapGet("/api/security/hash", (string password) =>
{
    return Results.Ok(new
    {
        Bad = PasswordHashing.HashPasswordBad(password),
        Good = PasswordHashing.HashPasswordPbkdf2(password)
    });
});

app.MapRazorPages();

app.Run();
