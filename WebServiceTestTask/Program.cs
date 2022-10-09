using WebServiceTestTask;


var builder = WebApplication.CreateBuilder(args);

var sender = new MessageSender("appsettings.Mail.json",MailDomain.Gmail);

var app = builder.Build();

//TODO: создать get запрос, возращать все сообщения 

app.MapPost("/api/mails", ((Message message) =>
{
    sender.Mailing(message.recipients);
    //TODO: создать сообщение и отправить его
    //TODO: внести записть в бд
}));

app.Map("/",async (context) =>
{
    await context.Response.SendFileAsync("wwwroot/index.html");
});
app.Run();
