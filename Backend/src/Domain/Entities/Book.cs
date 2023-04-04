using Domain.Common;

namespace Domain.Entities;

public sealed class Book : BaseEntity
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public string Cover { get; set; }
}