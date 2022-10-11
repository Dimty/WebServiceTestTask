using WebServiceTestTask.PostgresContext;

namespace WebServiceTestTask
{
    /// <summary>
    /// 
    /// </summary>
    public record LetterPostResponseStatus
    {
        public Result Status { get; set; }

        public int StatusCode { get; set; }
        public string? Description { get; set; }
        
    }
}