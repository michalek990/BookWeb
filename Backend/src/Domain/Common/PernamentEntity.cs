using Domain.Common.Interfaces;

namespace Domain.Common;

public abstract class PernamentEntity : BaseEntity, ISoftlyDeletable
{
    public bool Deleted { get; set; }
    public long? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
}