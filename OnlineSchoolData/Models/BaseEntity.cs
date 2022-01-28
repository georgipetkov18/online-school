namespace OnlineSchoolData.Models
{
    internal class BaseEntity
    {
        public string Id { get; set; } = new Guid().ToString();
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime DeletedOn { get; set; }
        public bool IsDeleted{ get; set; }
    }
}
