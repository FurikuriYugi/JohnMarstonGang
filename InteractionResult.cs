public class InteractionResult
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;

    public static InteractionResult Succeeded(string message)
    {
        return new InteractionResult
        {
            IsSuccess = true,
            Message = message
        };
    }

    public static InteractionResult Failed(string message)
    {
        return new InteractionResult
        {
            IsSuccess = false,
            Message = message
        };
    }
}