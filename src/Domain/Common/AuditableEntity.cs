namespace CleanArchitecture.Domain.Common;

public abstract class AuditableEntity
{
    public DateTime Created { get; set; }
    public User CreatedBy { get; set; }
    public Guid CreatedById { get; set; }

    public DateTime? LastModified { get; set; }

}
