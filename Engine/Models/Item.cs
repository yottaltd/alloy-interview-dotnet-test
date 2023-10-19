using System.Collections.Generic;

namespace Engine.Models
{
    public record Item(string Id, string DesignCode, IReadOnlyCollection<ItemAttribute> Attributes);
}