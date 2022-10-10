using System.Text.Json;
using WebServiceTestTask;
using WebServiceTestTask.PostgresContext;

var builder = WebApplication.CreateBuilder(args);

var sender = new MessageSender("appsettings.Mail.json",MailDomain.Gmail);

var dbContext = new MessagesContext();

var app = builder.Build();

var jsonSerializerOptions = new JsonSerializerOptions { Converters = { new DateOnlyJsonConverter() } };

app.MapGet("/api/mails", () => 
    JsonSerializer.Serialize(dbContext.MessageProperties, jsonSerializerOptions));

app.MapPost("/api/mails", ((Message message) =>
{
    sender.Mailing(message);
    var mess = new MessageProperty()
    {
        Subject = message.subject,
        Body = message.body,
        DateOfCreation = DateOnly.FromDateTime(DateTime.Now),
        Result = Result.OK,
        Recipients = message.recipients,
        FailedMessage = ""
    };
    
    dbContext.Add(mess);

    SaveChanges();
    //TODO: создать сообщение и отправить его
    //TODO: внести записть в бд
}));

app.Map("/",async (context) =>
{
    await context.Response.SendFileAsync("wwwroot/index.html");
});
app.Run();

async Task SaveChanges()
{
    await dbContext.SaveChangesAsync();
}
