using System.Net;
using System.Net.Mail;

namespace WebServiceTestTask
{
    /// <summary>
    /// This class is engaged in creating and sending messages.
    /// </summary>
    public class MessageSender
    {
        private readonly string _sender;
        private SmtpClient _client;
        /// <summary>
        /// The <see cref="SmtpClient">client</see> responsible for sending messages is initialized
        /// in the constructor.
        /// </summary>
        /// <param name="jsonPath">the path to the mail configuration file</param>
        /// <param name="mailDomain"><see cref="MailDomain"></see></param>
        public MessageSender(string jsonPath,MailDomain mailDomain)
        {
            var configuration =
                new ConfigurationBuilder().AddJsonFile(jsonPath, true).Build();
            _sender = configuration[mailDomain + ":login"];
            InitSmtpClient(configuration,mailDomain);
        }
        //TODO: после подкл бд заменить строку на нормальные данные
        public void Mailing(string[] addresses)
        {
            foreach (var item in addresses)
            {
                var mess = CreateMessage(item);
                _client.Send(mess);
            }
        }
        private void InitSmtpClient(IConfiguration configuration,MailDomain mailDomain)
        {
            _client = new SmtpClient();
            _client.EnableSsl = true;
            _client.Host = configuration[mailDomain + ":host"];
            _client.Port = int.Parse(configuration[mailDomain + ":port"]);
            _client.UseDefaultCredentials = false;
            _client.DeliveryMethod = SmtpDeliveryMethod.Network;
            _client.Credentials = new NetworkCredential()
            {
                UserName = _sender,
                Password = configuration[mailDomain + ":password"]
            };
        }
        private MailMessage CreateMessage(string mess)
        {
            MailMessage message = new MailMessage(_sender,mess);
            message.Body = mess;
            return message;
        }
    }
    
}