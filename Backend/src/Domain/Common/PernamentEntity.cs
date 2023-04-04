using Domain.Common.Interfaces;

namespace Domain.Common;

public abstract class PernamentEntity : BaseEntity, IUpdatable, ISoftlyDeletable
{
    public long? UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool Deleted { get; set; }
    public long? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
}