using Domain.Common;

namespace Domain.Entities;

public sealed class Review : PernamentEntity
{
    public int? Rate { get; set; }
    public string? Content { get; set; }
}