using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolData.Entities
{
    public class StudentEntity : UserEntity
    {
        public virtual ClassEntity Class { get; set; } = null!;
    }
}
