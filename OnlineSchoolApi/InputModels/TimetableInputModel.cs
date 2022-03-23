using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolApi.InputModels
{
    public class TimetableInputModel
    {
        [Required]
        public IEnumerable<TimetableEntryInputModel> Entries { get; set; } = null!;
    }
}
