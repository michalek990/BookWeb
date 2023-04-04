using Domain.Common;

namespace Domain.Entities;

public class Book : BaseEntity
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public string Cover { get; set; }
    
    public virtual List<Review> Reviews { get; set; }
}