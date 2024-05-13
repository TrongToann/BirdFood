namespace BirdFood.Contract.Abstraction.Shared
{
    public record Error
    {
        public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);
        public static readonly Error NullValue = new("Error.NullValue", "Null Value", ErrorType.Failure);
        public string Code { get; }
        public string Description { get; }
        public ErrorType Type { get; }
        public Error(string code, string description, ErrorType errorType = ErrorType.Failure)
        {
            Code = code;
            Description = description;
            Type = errorType;
        }
        public static Error NotFound(string code, string description) =>
            new Error(code, description, ErrorType.NotFound);
        public static Error Validation(string code, string description) =>
            new Error(code, description, ErrorType.Validation);
        public static Error Failure(string code, string description) =>
            new Error(code, description, ErrorType.Failure);
        public static Error Conflict(string code, string description) =>
            new Error(code, description, ErrorType.Conflict);
    }
    public enum ErrorType
    {
        Failure = 500,
        Validation = 400,
        NotFound = 404,
        Conflict = 409,
    }
}
