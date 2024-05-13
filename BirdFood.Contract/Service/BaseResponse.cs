namespace BirdFood.Contract.Service
{
    public class BaseResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public static BaseResponse BuildSuccessResponse(string message)
        {
            return new BaseResponse
            {
                Success = true,
                Errors = new List<string>(),
                Message = message
            };
        }
    }
}
