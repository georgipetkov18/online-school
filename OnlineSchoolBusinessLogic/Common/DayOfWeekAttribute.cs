using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolBusinessLogic.Common
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
    public class DayOfWeekAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var valueString = value?.ToString()!;
            var capitalized = char.ToUpper(valueString[0]) + valueString[1..].ToLower();
            return Enum.TryParse(capitalized, out DayOfWeek _);
        }
    }
}
