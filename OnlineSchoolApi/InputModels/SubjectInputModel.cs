namespace OnlineSchoolApi.RequestModels
{
    public class SubjectInputModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
    }
}
