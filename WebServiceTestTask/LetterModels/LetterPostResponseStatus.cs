using WebServiceTestTask.PostgresContext;

namespace WebServiceTestTask
{
    /// <summary>
    /// Stores the status status of the letter.
    /// </summary>
    public record LetterPostResponseStatus
    {
        /// <summary>
        /// Letter state.
        /// </summary>
        public Result Status { get; set; }
        /// <summary>
        /// State of letter.
        /// </summary>
        public int StatusCode { get; set; }
        /// <summary>
        /// Description of the status of the letter.
        /// </summary>
        public string? Description { get; set; }
        
    }
}