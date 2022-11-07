namespace Zip.InstallmentsApi.Model
{
    public class Error
    {
        public Error(int errorCode, string errorMessage, string? errorDetails = null)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            ErrorDetails = errorDetails;
        }

        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string? ErrorDetails { get; set; }
    }
}
