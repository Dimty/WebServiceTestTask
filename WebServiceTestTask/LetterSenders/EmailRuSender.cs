namespace WebServiceTestTask
{
    /// <summary>
    /// To send emails from the post Email.ru.
    /// </summary>
    public class EmailRuSender:LetterSender
    {
        protected override void SetDomain()
        {
            _domain = MailDomain.EmailRu;
        }
    }
}