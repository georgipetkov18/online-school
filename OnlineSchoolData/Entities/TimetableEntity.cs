namespace OnlineSchoolData.Entities
{
    public class TimetableEntity : BaseEntity
    {
        public virtual LessonEntity Lesson { get; set; } = null!;

        public virtual ClassInfoEntity ClassInfo { get; set; } = null!;
    }
}
