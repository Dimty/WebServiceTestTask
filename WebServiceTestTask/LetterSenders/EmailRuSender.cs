namespace WebServiceTestTask
{
    public class EmailRuSender:LetterSender
    {
        protected override void SetDomain()
        {
            _domain = MailDomain.EmailRu;
        }
    }
}