var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/api/mails", () => "asdas");
app.MapPost("/api/mails", () =>
{
    
});
app.Map("/",async (context) =>
{
    await context.Response.SendFileAsync("wwwroot/index.html");
});
app.Run();
