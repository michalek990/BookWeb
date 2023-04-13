using Domain.Common;

namespace Domain.Entities;

public class Review : BaseEntity
{
    public long BookId { get; set; }
    public virtual Book Book { get; set; }
    
    public long UserId { get; set; }
    public virtual User User { get; set; }
    
    public int? Rate { get; set; }
    public string? Content { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public long? UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
}