namespace aYoTechTest.BR.Models.Interfaces
{
    public interface IEntityBase
    {
        string CreatedById { get; set; }
        DateTime CreatedAt { get; set; }
        string AuthorisedById { get; set; }
        DateTime? AuthorisedAt { get; set; }
        string LastUpdatedById { get; set; }
        DateTime? LastUpdatedAt { get; set; }
        string DeletedById { get; set; }
        DateTime? DeletedAt { get; set; }

    }

}
