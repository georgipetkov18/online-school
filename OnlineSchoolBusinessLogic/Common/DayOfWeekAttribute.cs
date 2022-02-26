using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolBusinessLogic.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DayOfWeekAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var valueString = value?.ToString()!;
            var capitalized = char.ToUpper(valueString[0]) + valueString.Substring(1).ToLower();
            return Enum.TryParse(capitalized, out DayOfWeek _);
        }
    }
}
