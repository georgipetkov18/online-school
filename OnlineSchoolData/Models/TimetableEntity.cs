namespace OnlineSchoolData.Models
{
    internal class TimetableEntity : BaseEntity
    {
        public virtual Lesson Lesson { get; set; }

        public virtual ClassInfo ClassInfo { get; set; }
    }
}
