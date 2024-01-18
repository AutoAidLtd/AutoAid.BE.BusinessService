namespace AutoAid.Application.Common
{
    public static class ExceptionExtensions
    {
        public static string GetExceptionMessage(this Exception ex)
        {
            string errorMessage = ex.Message;
            if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
            {
                errorMessage += (" => " + ex.InnerException.Message);
                if (ex.InnerException.InnerException != null
                    && !string.IsNullOrEmpty(ex.InnerException.InnerException.Message))
                {
                    errorMessage += (" => " + ex.InnerException.InnerException.Message);
                    if (ex.InnerException.InnerException.InnerException != null
                    && !string.IsNullOrEmpty(ex.InnerException.InnerException.InnerException.Message))
                    {
                        errorMessage += (" => " + ex.InnerException.InnerException.InnerException.Message);
                    }
                }
            }
            return errorMessage;
        }
    }
}
