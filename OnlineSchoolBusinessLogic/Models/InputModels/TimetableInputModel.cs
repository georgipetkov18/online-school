using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolBusinessLogic.InputModels
{
    public class TimetableInputModel
    {
        [Required]
        public IEnumerable<TimetableEntryInputModel> Entries { get; set; } = null!;
    }
}
