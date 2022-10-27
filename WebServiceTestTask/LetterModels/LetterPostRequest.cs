namespace WebServiceTestTask
{
    /// <summary>
    /// This class contains properties describing the sender,
    /// the body of the message and the recipients.
    /// </summary>
    public record LetterPostRequest
    {
        /// <summary>
        /// Property <c>subject</c> sets or gets
        /// the name of the subject who sent the request.
        /// </summary>
        public string? Subject { get; set; }
        /// <summary>
        /// Property <c>body</c> sets or gets
        /// the body of the request sent by the subject.
        /// </summary>
        public string? Body { get; set; }
        /// <summary>
        /// Property <c>recipients</c> sets or gets
        /// recipients of the message body from the subject.
        /// </summary>
        public string[]? Recipients { get; set; }
    }
}
