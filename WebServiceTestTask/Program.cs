using System.Text.Json;
using WebServiceTestTask;
using WebServiceTestTask.PostgresContext;


string exceptionResponse = "An error occurred while processing the request";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<MessagesDbContext>();

builder.Services.AddSingleton<LetterSender, GmailSender>();

var app = builder.Build();

var jsonSerializerOptions = new JsonSerializerOptions {Converters = {new DateOnlyJsonConverter()}};

app.MapGet("/api/mails", () =>
{
    var db = app.Services.GetService<MessagesDbContext>();
    return JsonSerializer.Serialize(db?.MessageProperties, jsonSerializerOptions);
});

app.MapPost("/api/mails", ((LetterPostRequest message) =>
{
    var sender = app.Services.GetService<LetterSender>();
    var db = app.Services.GetService<MessagesDbContext>();

    var letterStatus = sender?.CheckLetterRequest(message);

    if (letterStatus?.Status == Result.OK)
    {
        letterStatus = sender?.Mailing(message);
        if (letterStatus?.Status == Result.OK)
        {
            db.SaveMessage(message, letterStatus);
            return Results.Ok();
        }

        letterStatus.StatusCode = 520;
    }
    else letterStatus.StatusCode = StatusCodes.Status400BadRequest;

    db.SaveMessage(message, letterStatus);
    return Results.Problem(exceptionResponse, letterStatus.Description,letterStatus.StatusCode);
}));

app.Map("/", async (context) => { await context.Response.SendFileAsync("wwwroot/index.html"); });
app.Run();
