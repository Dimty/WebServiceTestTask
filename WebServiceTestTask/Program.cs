using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using WebServiceTestTask;
using WebServiceTestTask.PostgresContext;

string exceptionResponse = "An error occurred while processing the request";

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("MessagesDbContext");

builder.Services.AddDbContext<MessagesDbContext>(options =>
    options.UseNpgsql(connection ?? throw new InvalidOperationException("Connection string 'WebAppMVCContext' not found.")));

builder.Services.Configure<MailOption>(builder.Configuration.GetSection(MailOption.Mails + ":Gmail"));

builder.Services.AddTransient<LetterSender>();

var app = builder.Build();

var jsonSerializerOptions = new JsonSerializerOptions {Converters = {new DateOnlyJsonConverter()}};

app.MapGet("/api/mails", (MessagesDbContext context, HttpContext resp) =>
    resp.Response.WriteAsJsonAsync(context.MessageProperties, jsonSerializerOptions));

app.MapPost("/api/mails", async (LetterPostRequest message, LetterSender sender, MessagesDbContext db) =>
{
    //TODO: change data validation
    var letterStatus = sender?.CheckLetterRequest(message);

    if (letterStatus?.Status == Result.OK)
    {
        letterStatus = sender?.Mailing(message);
        if (letterStatus?.Status == Result.OK)
        {
            await db.SaveMessageAsync(message, letterStatus);
            return Results.Ok();
        }

        letterStatus.StatusCode = 520;
    }
    else letterStatus.StatusCode = StatusCodes.Status400BadRequest;

    await db.SaveMessageAsync(message, letterStatus);
    return Results.Problem(exceptionResponse, letterStatus.Description, letterStatus.StatusCode);
});

app.Run();