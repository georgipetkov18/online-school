namespace OnlineSchoolData.CustomExceptions
{
    public abstract class CustomException : Exception 
    {
        public CustomException(string message) 
            : base(message) { }
    }

}
