namespace OnlineSchoolData.Entities
{
    public class RefreshTokenEntity : BaseEntity
    {
        public string Token { get; set; } = null!;

        public DateTime ExpiresOn { get; set; }

        public virtual UserEntity User { get; set; } = null!;
        public Guid UserId { get; set; }
    }
}
