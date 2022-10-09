namespace WebServiceTestTask
{
    /// <summary>
    /// This class contains properties describing the sender,
    /// the body of the message and the recipients.
    /// </summary>
    public record Message
    {
        /// <summary>
        /// Property <c>subject</c> sets or gets
        /// the name of the subject who sent the request.
        /// </summary>
        public string subject { get; set; }
        /// <summary>
        /// Property <c>body</c> sets or gets
        /// the body of the request sent by the subject.
        /// </summary>
        public string body { get; set; }
        /// <summary>
        /// Property <c>recipients</c> sets or gets
        /// recipients of the message body from the subject
        /// </summary>
        public string[] recipients { get; set; }
    }
}
/// <exception cref="System.OverflowException">
/// Thrown when one parameter is 
/// <see cref="Int32.MaxValue">MaxValue</see> and the other is
/// greater than 0.
/// Note that here you can also use 
/// <see href="https://docs.microsoft.com/dotnet/api/system.int32.maxvalue"/>
///  to point a web page instead.
/// </exception>
/// 
/// <summary>
/// The main <c>Math</c> class.
/// Contains all methods for performing basic math functions.
/// <list type="bullet">
/// <item>
/// <term>Add</term>
/// <description>Addition Operation</description>
/// </item>
/// <item>
/// <term>Subtract</term>
/// <description>Subtraction Operation</description>
/// </item>
/// <item>
/// <term>Multiply</term>
/// <description>Multiplication Operation</description>
/// </item>
/// <item>
/// <term>Divide</term>
/// <description>Division Operation</description>
/// </item>
/// </list>
/// </summary>
/// <remarks>
/// <para>
/// This class can add, subtract, multiply and divide.
/// </para>
/// <para>
/// These operations can be performed on both
/// integers and doubles.
/// </para>
/// </remarks>