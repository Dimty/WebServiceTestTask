using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using WebServiceTestTask.PostgresContext;

namespace WebServiceTestTask
{
    /// <summary>
    /// This class is engaged in creating and sending messages.
    /// </summary>
    public class LetterSender
    {
        private readonly SmtpClient? _client= new();

        private readonly MailOption _option;
        public LetterSender(IOptions<MailOption> options)
        {
            _option = options.Value;
            InitSmtpClient();
        }

        /// <summary>
        /// Sends a letter to the recipients.
        /// </summary>
        /// <param name="letterPostRequest">Letter to send</param>
        /// <returns>The status of sending the letter.</returns>
        public LetterPostResponseStatus Mailing(LetterPostRequest letterPostRequest)
        {
            var obj = new object();
            var status = new LetterPostResponseStatus();
            try
            {
                var mess = CreateMessage(letterPostRequest);
                foreach (var item in letterPostRequest.Recipients)
                {
                    mess.To.Add(item);
                }
                _client?.SendAsync(mess,obj);
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
        /// Checks the data of the letter for correctness.
        /// </summary>
        /// <param name="letter"></param>
        /// <returns>The status of the email verification.</returns>
        public LetterPostResponseStatus CheckLetterRequest(LetterPostRequest letter)
        {
            var status = new LetterPostResponseStatus();
            if (string.IsNullOrWhiteSpace(letter.Subject) ||
                string.IsNullOrWhiteSpace(letter.Body) ||
                letter.Recipients is null)
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
            message.From = new MailAddress(_option.Login);
            message.Subject = mess.Subject;
            message.Body = mess.Body;
            return message;
        }

        private void InitSmtpClient()
        {
            _client.EnableSsl = true;
            _client.Host = _option.Host;
            _client.Port = _option.Port;
            _client.UseDefaultCredentials = false;
            _client.DeliveryMethod = SmtpDeliveryMethod.Network;
            _client.Credentials = new NetworkCredential()
            {
                UserName = _option.Login,
                Password = _option.Password
            };
        }
    }
}