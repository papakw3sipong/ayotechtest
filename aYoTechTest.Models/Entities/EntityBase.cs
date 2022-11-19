using aYoTechTest.BR.Models.Interfaces;

namespace aYoTechTest.Models.Entities
{
    public abstract class EntityBase : IEntityBase
    {
        public string CreatedById { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AuthorisedById { get; set; }
        public DateTime? AuthorisedAt { get; set; }
        public string LastUpdatedById { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public string DeletedById { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}

