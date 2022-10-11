namespace WebServiceTestTask
{
    public class GmailSender:LetterSender
    {
        protected override void SetDomain()
        {
            _domain = MailDomain.Gmail;
        }
    }
}