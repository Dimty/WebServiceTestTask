using System.Net;
using System.Net.Mail;
using WebServiceTestTask.PostgresContext;

namespace WebServiceTestTask
{
    /// <summary>
    /// This class is engaged in creating and sending messages.
    /// </summary>
    public abstract class LetterSender
    {
        private readonly string _sender;
        private readonly string _configPath = "appsettings.Mail.json";
        private SmtpClient? _client;
        protected MailDomain _domain;

        /// <summary>
        /// The <see cref="SmtpClient">client</see> responsible for sending messages is initialized
        /// in the constructor.
        /// </summary>
        protected LetterSender()
        {
            var configuration =
                new ConfigurationBuilder().AddJsonFile(_configPath, true).Build();

            _sender = configuration[_domain + ":login"];
            InitSmtpClient(configuration, _domain);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="letterPostRequest"></param>
        /// <returns></returns>
        public LetterPostResponseStatus Mailing(LetterPostRequest letterPostRequest)
        {
            var status = new LetterPostResponseStatus();
            try
            {
                var mess = CreateMessage(letterPostRequest);
                foreach (var item in letterPostRequest.recipients)
                {
                    mess.To.Add(item);
                }
                _client?.Send(mess);
            }
            catch (Exception e)
            {
                status.Status = Result.Failed;
                status.Description = "There were problems during the delivery of the message. Repeat the operation later.";
                return status;
            }

            status.Status = Result.OK;
            return status;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="letter"></param>
        /// <returns></returns>
        public LetterPostResponseStatus CheckLetterRequest(LetterPostRequest letter)
        {
            var status = new LetterPostResponseStatus();
            if (string.IsNullOrWhiteSpace(letter.subject) ||
                string.IsNullOrWhiteSpace(letter.body) ||
                letter.recipients is null)
            {
                status.Description = "One or more incoming parameters are missing";
                status.Status = Result.Failed;
                return status;
            }
            status.Status = Result.OK;
            return status;
        }

        private MailMessage CreateMessage(LetterPostRequest mess)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(_sender);
            message.Subject = mess.subject;
            message.Body = mess.body;
            return message;
        }

        private void InitSmtpClient(IConfiguration configuration, MailDomain mailDomain)
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

        protected abstract void SetDomain();
    }
}