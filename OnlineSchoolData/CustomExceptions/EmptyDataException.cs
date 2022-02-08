namespace OnlineSchoolData.CustomExceptions
{
    public class EmptyDataException : CustomException
    {
        public EmptyDataException(string parameterName) 
            : base($"Data for parameter: {parameterName} cannot be empty!") { }
    }
}
