namespace WebServiceTestTask
{
    /// <summary>
    /// To send emails from the post Gmail.com
    /// </summary>
    public class GmailSender:LetterSender
    {
        protected override void SetDomain()
        {
            _domain = MailDomain.Gmail;
        }
    }
}