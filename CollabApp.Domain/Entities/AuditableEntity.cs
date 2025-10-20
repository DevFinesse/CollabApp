namespace CollabApp.Domain.Entities
{
    public class AuditableEntity
    {
        public Guid CreatedById { get; set; } 
        public User CreatedBy { get; set; } = default!;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public Guid UpdatedById { get; set; }
        public User? UpdatedBy { get; set; }
        public DateTime? JoinedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
    }
}
