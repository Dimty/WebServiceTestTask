namespace WebServiceTestTask
{
    /// <summary>
    /// An automatically generated class describing columns in the database.
    /// </summary>
    public partial class MessageProperty
    {
        public int Id { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public string[]? Recipients { get; set; }
        public DateOnly? DateOfCreation { get; set; }
        public string? FailedMessage { get; set; }
        public PostgresContext.Result Result { get; set; } 
    }
}
