namespace OnlineSchoolData.CustomExceptions
{
    public class InvalidDataProvidedException : CustomException
    {
        public InvalidDataProvidedException(string parameterName) 
            : base($"Invalid data for parameter: {parameterName} was provided!") { }

        public InvalidDataProvidedException(string parameterName, string parameterValue)
            :base($"Data: {parameterValue} is invalid for parameter: {parameterName}") { }

    }
}
