namespace Web.Models;

public class ErrorViewModel
{
    public int StatusCode { get; set; }
    public string Title { get; set; } = "Error";
    public string Message { get; set; } = "An error occurred while processing your request.";
    public string? RequestId { get; set; }
    public Exception? Exception { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    public bool ShowException => Exception != null;
}
