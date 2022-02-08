namespace OnlineSchoolData.CustomExceptions
{
    public class InvalidIdException : CustomException
    {
        public InvalidIdException(string message) 
            : base(message) { }

        public InvalidIdException(string message, Guid invalidId) 
            : base($"{message} - Id: {invalidId}") { }

    }
}
