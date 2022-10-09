var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.Map("/",async (context) =>
{
    await context.Response.SendFileAsync("wwwroot/index.html");
});
app.Run();
